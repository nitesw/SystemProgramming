namespace Lesson_05_AsyncAwait
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread Id {0} start", Thread.CurrentThread.ManagedThreadId);
            MyClass myClass = new MyClass();

            //myClass.Operation();
            myClass.OperationAsync();

            Console.WriteLine("Main Thread Id {0} end", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
