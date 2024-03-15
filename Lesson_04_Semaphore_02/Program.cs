using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Xml.Serialization;

namespace Lesson_04_Semaphore_02
{
    internal class Program
    {
        private static Semaphore semaphore = new Semaphore(5, 5);
        private static List<PlayerStats> players = new List<PlayerStats>();
        const int balance = 1000;

        private static void SaveStatsToFile(string path)
        {
            if(!File.Exists(path))
            {
                try
                {
                    string json = JsonSerializer.Serialize(players);

                    File.WriteAllText(path, json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void PlayCasino()
        {
            int money = balance;
            int numberOfPlays = new Random().Next(1, 5);

            semaphore.WaitOne();

            for (int i = 0; i < numberOfPlays; i++)
            {
                if (money > 100)
                {
                    int winnerNum = new Random().Next(0, 20);
                    int betNum = new Random().Next(0, 20);
                    int bet = new Random().Next(100, money);

                    Console.WriteLine($"{Thread.CurrentThread.Name} is betting on number {betNum} with {bet}$ bet. Current balance {money}$.");
                    Thread.Sleep(2000);
                    if (betNum == winnerNum)
                    {
                        Console.WriteLine($"Winner num is {winnerNum}. {Thread.CurrentThread.Name} won.");
                        money = (money - bet) + bet * 2;
                    }
                    else
                    {
                        Console.WriteLine($"Winner num is {winnerNum}. {Thread.CurrentThread.Name} lost.");
                        money -= bet;
                    }
                }
                else
                {
                    players.Add(new PlayerStats(Thread.CurrentThread.Name, balance, money));
                    Console.WriteLine($"{Thread.CurrentThread.Name} is leaving the roulette table because of his low balance ({money}$).");
                    break;
                }
            }

            if(money > 100)
            {
                players.Add(new PlayerStats(Thread.CurrentThread.Name, balance, money));
                Console.WriteLine($"{Thread.CurrentThread.Name} is leaving the roulette after these many plays: {numberOfPlays}. Balance {money}$.");
            }

            semaphore.Release();
        }

        static void Main(string[] args)
        {
            string filePath = "Players.json";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < new Random().Next(20, 100); i++)
            {
                Thread thread = new Thread(PlayCasino);
                thread.Name = $"Player {i}";
                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            SaveStatsToFile("Players.json");
        }
    }
}
