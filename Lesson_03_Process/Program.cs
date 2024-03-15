using System.Diagnostics;

namespace Lesson_03_Process
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*var process = Process.GetProcesses();

                        foreach (var p in process)
                        {
                            Console.WriteLine($"{p.Id} <---> {p.ProcessName}");
                        }*/

            /*var process = Process.GetProcessesByName("chrome");

            foreach (var p in process)
            {
                Console.WriteLine(p.ProcessName);
                Console.WriteLine(p.MainWindowTitle);
                Console.WriteLine(p.PrivateMemorySize);
            }*/

            var processInfo = new ProcessStartInfo
            {
                FileName = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
                //WindowStyle = ProcessWindowStyle.Maximized,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "youtube.com"
            };

            using (Process process = Process.Start(processInfo))
            {
                Thread.Sleep(10000);
                process.Kill();
            }
        }
    }
}
