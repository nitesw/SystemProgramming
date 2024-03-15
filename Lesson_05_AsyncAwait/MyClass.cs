using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_05_AsyncAwait
{
    internal class MyClass
    {
        public void Operation()
        {
            Console.WriteLine("Operation ThreadId {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Begin...");
            Thread.Sleep(2000);
            Console.WriteLine("End...");
        }

        public async void OperationAsync()
        {
            Task task = new Task(Operation);
            task.Start();

            await task;
        }
    }
}
