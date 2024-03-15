namespace Lesson_04_Semaphore_01
{
    internal class Program
    {
        static int count = 0;
        private static Semaphore semaphore = new Semaphore(3, 3);

        private static void RandomNumbersGenerator()
        {
            semaphore.WaitOne();

            List<int> list = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                list.Add(new Random().Next(0, 50));
            }

            Interlocked.Increment(ref count);
            Console.WriteLine($"{Thread.CurrentThread.Name}. Random nums: {string.Join(' ', list)}");

            Thread.Sleep(new Random().Next(2000, 5000));

            Console.WriteLine($"{Thread.CurrentThread.Name} finished working.");
            Interlocked.Decrement(ref count);

            semaphore.Release();
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(RandomNumbersGenerator);
                thread.Name = $"Thread number {i}";
                thread.Start();
            }
        }
    }
}
