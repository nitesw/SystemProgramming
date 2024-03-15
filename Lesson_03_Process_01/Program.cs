using System;
using System.Diagnostics;

namespace Lesson_03_Process_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Console.Write("Enter the time to refresh processes (in sec): ");
            int time = int.Parse(Console.ReadLine());
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                var process = Process.GetProcesses();

                foreach (var p in process)
                {
                    Console.WriteLine($"{p.Id} <---> {p.ProcessName}");
                }
                Console.WriteLine("\n\n\t\tPress 'Esc' to exit!");

                Thread.Sleep(time * 1000);
                Console.Clear();
            }*/

            /*bool restart = true;
            int id = 0;
            var processes = Process.GetProcesses();
            while (true)
            {
                if (restart)
                {
                    Console.Clear();

                    processes = Process.GetProcesses();
                    foreach (var p in processes)
                    {
                        Console.WriteLine($"Process id: {p.Id}. Process name: {p.ProcessName}");
                    }       
                    
                    Console.Write("\n\tEnter process's id to see info about (enter '-1' to exit): ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        if (id == -1)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\tInvalid input.");
                        break;
                    }
                }

                try
                {
                     var process = Process.GetProcessById(id);

                     Console.Clear();

                     Console.WriteLine($"Process id: {process.Id}");
                     Console.WriteLine($"Process name: {process.ProcessName}");
                     Console.WriteLine($"Process start time: {process.StartTime}");
                     Console.WriteLine($"Process total processor time: {process.TotalProcessorTime}");
                     Console.WriteLine($"Process total amount of threads: {process.Threads.Count}");
                     Console.WriteLine($"Process total amount of copies: {process.Modules.Count}");

                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("\n\t" + ex.Message);
                    break;
                }

                Console.Write("Enter any symbol to exit to processes (enter '-1' to exit or 's' to show processes list or 'd' to delete process): ");
                string choice = Console.ReadLine();
                if (choice == "s")
                {
                    Console.Clear();
                    restart = true;
                }
                else if (choice == "d")
                {
                    var process = Process.GetProcessById(id);
                    process.Kill();
                    Console.Clear();
                    restart = true;
                }
                else
                {
                    if (int.TryParse(choice, out id))
                    {
                        if (id == -1)
                        {
                            break;
                        }
                    }
                    Console.Clear();
                    restart = false;
                }
            }*/

            int choice;
            string path = "D:\\Programs\\Steam\\steam.exe";
            while (true)
            {  
                Console.WriteLine("\n\t1. Notepad");
                Console.WriteLine("\n\t2. Calculator");
                Console.WriteLine("\n\t3. Paint");
                Console.WriteLine("\n\t4. Steam");
                Console.WriteLine("\n\t5. Exit");
                Console.Write("\n\tEnter your choice: ");

                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 4)
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Process process = Process.Start("notepad.exe");
                            break;
                        case 2:
                            Console.Clear();
                            process = Process.Start("calc.exe");
                            break;
                        case 3:
                            Console.Clear();
                            process = Process.Start("mspaint.exe");
                            break;
                        case 4:
                            Console.Clear();
                            process = Process.Start(path);
                            break;
                    }
                }
                else if(choice == 5)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\tEnter proper number!\n");
                }
            }
        }
    }
}
