using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text.Json;

namespace Exam
{
    internal class Program
    {
        private static bool stopSearch = false;
        private static List<string> filesPath = new List<string>();

        static void DrawProgressBar(int process, int total)
        {
            Console.Write("\r[");

            int progress = (int)((double)process / total * 50);

            for (int i = 0; i < progress; i++)
            {
                Console.Write("#");
            }

            for (int i = progress; i < 50; i++)
            {
                Console.Write("-");
            }

            Console.Write($"] {process}/{total}");
            Console.WriteLine();
        }

        static async Task<List<FoundWord>> FindWord(string word, string path)
        {
            List<FoundWord> foundWords = new List<FoundWord>();
            string choice = string.Empty;
            int process = 0;
            var files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories);

            try
            {
                foreach (var file in files)
                {
                    Console.WriteLine($"Press 'Esc' to stop search.\n");
                    DrawProgressBar(process, files.Count());
                    Console.WriteLine($"Searching through file '{file}'...");
                    await Task.Delay(100);
                    bool foundInFile = false;
                    bool repeatInFile = false;
                    int count = 0;

                    foreach (var line in File.ReadLines(file))
                    {
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            stopSearch = true;
                            break;
                        }

                        if (line.Contains(word))
                        {
                            count++;
                            for (int i = 0; i < foundWords.Count; i++)
                            {
                                if (foundWords[i].FileName == Path.GetFileName(file))
                                {
                                    foundWords[i].Count++;
                                    repeatInFile = true;
                                }
                            }
                            if(!repeatInFile)
                            {
                                filesPath.Add(file);
                                foundWords.Add(new FoundWord { FileName = Path.GetFileName(file), FilePath = file, Count = count }); ;
                                foundInFile = true;
                            }
                        }
                    }

                    if (stopSearch)
                    {
                        Console.WriteLine("\nSearch stopped by user.");
                        break;
                    }

                    if (!foundInFile)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"Word [{word}] was not found in '{file}'");
                        await Task.Delay(200);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Clear();
                    }
                    else if (foundInFile)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Word [{word}] was found in '{file}'");
                        await Task.Delay(200);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Clear();
                    }
                    process++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return foundWords;
        }

        static async Task SaveToFile(List<FoundWord> words, string path)
        {
            try
            {
                string json = JsonSerializer.Serialize(words);

                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task SendToEmail(string path, string sender, string senderPass, string receiver)
        {  
            string host = "imap.ukr.net";
            int port = 993;

            try
            {
                MimeMessage mail = new MimeMessage();
                mail.From.Add(new MailboxAddress("SearchForWord", sender));
                mail.To.Add(new MailboxAddress("Receiver", receiver));
                mail.Subject = "Results";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Some info which I found is in !FoundWords.json file";
                bodyBuilder.Attachments.Add(path);
                foreach (var file in filesPath)
                {
                    bodyBuilder.Attachments.Add(file);
                }
                mail.Body = bodyBuilder.ToMessageBody();

                using (var client = new ImapClient())
                {
                    await client.ConnectAsync(host, port, true);

                    await client.AuthenticateAsync(sender, senderPass);

                    var folder = await client.GetFolderAsync("INBOX");

                    await folder.AppendAsync(mail);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task ShowFiles(List<FoundWord> res)
        {
            foreach (var file in res)
            {
                Console.WriteLine($"\nFile name: {file.FileName}\nFile path: {file.FilePath}\nWords count: {file.Count}");
            }
        }

        static async Task Main(string[] args)
        {
            int choice;
            List<FoundWord> result = null;
            try
            {
                do
                {
                    Console.WriteLine("1. Search for word");
                    Console.WriteLine("2. Print statistics");
                    Console.WriteLine("3. Save stats to file and send to email");
                    Console.WriteLine("0. Exit");
                    Console.Write("Enter your choice: ");

                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Console.Write("Enter word you want to search for: ");
                            string wordToFind = Console.ReadLine();
                            Console.Write($"Enter path to directory where you want to search for the word [{wordToFind}]: ");
                            string pathToDirectory = Console.ReadLine();

                            Console.Clear();
                            result = await FindWord(wordToFind, pathToDirectory);
                            break;
                        case 2:
                            if (result == null)
                            {
                                Console.Clear();
                                Console.WriteLine("There is no found files to print");
                            }
                            else
                            {
                                Console.Clear();
                                await ShowFiles(result);
                                Console.WriteLine();
                            }
                            break;
                        case 3:
                            if (result == null)
                            {
                                Console.Clear();
                                Console.WriteLine("There is no found files to send to email");
                            }
                            else
                            {
                                Console.Clear();
                                await SaveToFile(result, "!FoundWords.json");
                                await SendToEmail("!FoundWords.json", "sendingmails@ukr.net", "r3LkTddNciFBAwoq", "sendingmails@ukr.net");
                                }
                            break;
                    }
                } while (choice != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }
    }
}
