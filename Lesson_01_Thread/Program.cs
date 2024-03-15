namespace Lesson_01
{
    internal class Program
    {
        static void RecursionOfNumbers(int num, int startNum = 1)
        {
            if (num >= 1)
            {
                if (startNum == num)
                {
                    Console.WriteLine(startNum);
                    Console.WriteLine("Ok");
                }
                else
                {
                    Console.WriteLine(startNum);
                    RecursionOfNumbers(num, startNum + 1);
                }
            }
            else Console.WriteLine("Error");
        }

        static void SecondThread()
        {
            while (true)
            {
                Console.WriteLine(new string(' ', 10) + "Second Thread");
            }
        }

        static void ThirdThread()
        {
            Thread thread = Thread.CurrentThread;
            thread.Name = "Third Thread";
            Console.WriteLine($"Id {thread.Name}: {thread.GetHashCode()}");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(new string(' ', 10) + thread.Name + i);
                Thread.Sleep(3000);
            }
        }

        static void Main(string[] args)
        {
            //Thread mainThread = Thread.CurrentThread;
            //mainThread.Name = "Main Thread";
            //Console.WriteLine($"Id {mainThread.Name}: {mainThread.GetHashCode()}");
            ////RecursionOfNumbers(1);

            //Thread secondThread = new Thread(SecondThread);
            ////secondThread.Start();
            //SecondThread();

            Thread thirdThread = new Thread(ThirdThread);
            thirdThread.Start();
        }
    }
}
