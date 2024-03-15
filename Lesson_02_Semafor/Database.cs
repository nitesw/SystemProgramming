using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_02_Semafor
{
    internal class Database
    {
        object locker = new object();
        public void LaunchRocket()
        {
            int hash = Thread.CurrentThread.GetHashCode();
            lock (locker)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Thread #{hash}: iterator {i}");
                    Thread.Sleep(1000);
                }
                Console.WriteLine(new string(' ', 20));
            }
        }
    }
}
