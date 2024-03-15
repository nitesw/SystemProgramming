namespace Lesson_06_Mutex
{
    internal class Program
    {
        static Mutex mutex = new Mutex();

        static void PrintNumsAsc()
        {
            Console.WriteLine("Thread #{0} requesting mutex...", Thread.CurrentThread.ManagedThreadId);
            mutex.WaitOne();

            Console.WriteLine("Thread #{0} accessing protected area...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            Console.Write("Asc nums (from 0 to 20): ");
            for (int i = 0; i < 21; i++)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();

            Console.WriteLine("Thread #{0} realising mutex...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            mutex.ReleaseMutex();
        }
        static void PrintNumsDesc()
        {
            Console.WriteLine("Thread #{0} requesting mutex...", Thread.CurrentThread.ManagedThreadId);
            mutex.WaitOne();

            Console.WriteLine("Thread #{0} accessing protected area...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            Console.Write("Desc nums (from 10 to 0): ");
            for (int i = 10; i >= 0; i--)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();

            Console.WriteLine("Thread #{0} realising mutex...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            mutex.ReleaseMutex();
        }

        static int[] ModifyNums(ref int[] arr, int addNum)
        {
            Console.WriteLine("Thread #{0} requesting mutex...", Thread.CurrentThread.ManagedThreadId);
            mutex.WaitOne();

            Console.WriteLine("Thread #{0} accessing protected area...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);

            Console.WriteLine("Increase all nums by {0}", addNum);
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] += addNum;
            }

            Console.WriteLine("Thread #{0} realising mutex...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            mutex.ReleaseMutex();

            return arr;
        }
        static int FindMaxNum(int[] arr)
        {
            Console.WriteLine("Thread #{0} requesting mutex...", Thread.CurrentThread.ManagedThreadId);
            mutex.WaitOne();

            Console.WriteLine("Thread #{0} accessing protected area...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);

            //Console.WriteLine("Finding max num in arr...");
            //Thread.Sleep(1000);
            int maxNum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if(maxNum < arr[i])
                {
                    maxNum = arr[i];
                }
            }
            //Console.WriteLine("Max num in arr: {0}", maxNum);

            Console.WriteLine("Thread #{0} realising mutex...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            mutex.ReleaseMutex();

            return maxNum;
        }

        static void Main(string[] args)
        {
            /*Thread threadAsc = new Thread(PrintNumsAsc);
            Thread threadDesc = new Thread(PrintNumsDesc);

            threadAsc.Start();
            threadDesc.Start();

            threadAsc.Join();
            threadDesc.Join();*/

            int size = new Random().Next(1, 10);
            int[] arr = new int[size];
            int addNum = new Random().Next(1, 5);

            for (int i = 0; i < size; i++)
            {
                arr[i] = new Random().Next(1, 20);
            }
            Console.WriteLine("Array: ");
            for (int i = 0; i < size; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();

            int[] valueModify = null;
            object valueMax = null;
            Thread threadModify = new Thread(() => { valueModify = ModifyNums(ref arr, addNum); });
            Thread threadMax = new Thread(() => { valueMax = FindMaxNum(arr); });

            threadModify.Start();
            threadModify.Join();
            Console.WriteLine("Modified array: ");
            for (int i = 0; i < valueModify.Length; i++)
            {
                Console.Write(valueModify[i] + " ");
            }
            Console.WriteLine();

            threadMax.Start();
            threadMax.Join();
            Console.WriteLine("Max num in array: {0}", valueMax);
        }
    }
}
