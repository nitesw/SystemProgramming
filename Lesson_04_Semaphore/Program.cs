namespace Lesson_04_Semaphore
{
    internal class Program
    {
        private static void Work()
        {
            semaphore.WaitOne();

            Interlocked.Increment(ref count);
            Console.WriteLine($"{Thread.CurrentThread.Name} starting... Now working {count} (count of threads)");
            Thread.Sleep(new Random().Next(2000, 5000));
            Console.WriteLine($"{Thread.CurrentThread.Name} finished... Now working {count} (count of threads)");
            Interlocked.Decrement(ref count);

            semaphore.Release();
        }

        static int count = 0;
        private static Semaphore semaphore = new Semaphore(5, 5);

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(Work);
                thread.Name = $"Thread number {i}";
                thread.Start();
            }
        }
    }
}
