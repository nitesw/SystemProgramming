using System.Diagnostics;

namespace Lesson_02_Semafor
{
    internal class Program
    {
        static void LaunchRocket(object obj)
        {
            Parts parts = obj as Parts;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Rocket {parts.Name} is launched with next params:\n{parts.Engine}\n{parts.FuelSystem}" +
                    $"\n{parts.Navigation}\n\n==================================");
                Thread.Sleep(parts.Timer);
            }
        }

        static void Main(string[] args)
        {
            /*Database db = new Database();

            Monitor.Enter(db);
            for (int i = 0; i < 3; i++)
            {
                new Thread(db.LaunchRocket).Start();
            }
            Monitor.Exit(db);
            Thread.Sleep(1000);

            Console.WriteLine("Main Thread Done!");*/

            /*Parts rocket = new Parts();
            rocket.Name = "DOS4GW";
            rocket.FuelSystem = "Bosh fuel";
            rocket.Navigation = "Space Maps navigation";
            rocket.Engine = "Biboo engine";
            rocket.Timer = 2000;

            ParameterizedThreadStart launchRocket = new ParameterizedThreadStart(LaunchRocket);
            Thread thread = new Thread(launchRocket);
            thread.Start(rocket);

            Console.ReadKey();*/
        }
    }
}
