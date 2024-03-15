using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Lesson_01_01
{
    internal class Program
    {
        static void RandomNumbersThread(int startNum, int endNum, string name)
        {
            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = name;
            Console.WriteLine(new string(' ', 10) + $"Name: {mainThread.Name} Id: {mainThread.GetHashCode()} " + 
                " Random Number: " + new Random().Next(startNum, endNum));
        }

        static int[] GenerateRandomNumbers(int count)
        {
            Random random = new Random();
            int[] numbers = new int[count];

            for (int i = 0; i < count; i++)
            {
                numbers[i] = random.Next(100000);
            }

            return numbers;
        }

        static int maxNum;
        static int minNum;
        static double avgNum;
        static int MaxNumThread(int[] arr)
        {
            maxNum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if(maxNum < arr[i])
                {
                    maxNum = arr[i];
                }
            }

            return maxNum;
        }
        static int MinNumThread(int[] arr)
        {
            minNum = arr[0];

            for (int i = 0; i < arr.Length; i++)
            {
                if(minNum > arr[i])
                {
                    minNum = arr[i];
                }
            }

            return minNum;
        }
        static double AvgNumThread(int[] arr)
        {
            avgNum = 0.0;

            for (int i = 0; i < arr.Length; i++)
            {
                avgNum += arr[i];
            }

            avgNum = avgNum / arr.Length;

            return avgNum;
        }
        static void PrintNumbersThread(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"\t{arr[i]}");
            }
        }
        static void SaveToFileThread(object obj, string type)
        {
            var num = obj;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(type + obj);

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));
                using (Stream stream = File.Open("Nums.xml", FileMode.Append))
                {
                    xmlSerializer.Serialize(stream, stringBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Main(string[] args)
        {
            /*int num1, num2;
            Console.Write("Start num: ");
            num1 = int.Parse(Console.ReadLine());

            Console.Write("End num: ");
            num2 = int.Parse(Console.ReadLine());

            Console.Write("Enter number of threads: ");
            int numberOfThreads = int.Parse(Console.ReadLine());

            if (num1 > num2)
            {
                int startNum = num2;
                int endNum = num1;
                if (numberOfThreads > 0)
                {
                    for (int i = 0; i < numberOfThreads; i++)
                    {
                        int threadNumber = i + 1;
                        Thread thread = new Thread(delegate () { RandomNumbersThread(startNum, endNum, "Thread " + threadNumber); });
                        thread.Start();
                    }
                }
                else Console.WriteLine("Error!");
            }
            else
            {
                int startNum = num1;
                int endNum = num2;
                if (numberOfThreads > 0)
                {
                    for (int i = 0; i < numberOfThreads; i++)
                    {
                        int threadNumber = i + 1;
                        Thread thread = new Thread(delegate () { RandomNumbersThread(startNum, endNum, "Thread " + threadNumber); });
                        thread.Start();
                    }
                }
                else Console.WriteLine("Error!");
            }

            //Thread firstThread = new Thread(RandomNumbersThread);
            //firstThread.Start();

            //Thread secondThread = new Thread(RandomNumbersThread);
            //secondThread.Start();

            //Thread thirdThread = new Thread(RandomNumbersThread);
            //thirdThread.Start();

            //Thread fourthThread = new Thread(RandomNumbersThread);
            //fourthThread.Start();

            //Thread fifthThread = new Thread(RandomNumbersThread);
            //fifthThread.Start();

            Console.WriteLine("Main thread is ended.");*/

            int[] randomNumbers = GenerateRandomNumbers(1000);

            Thread maxNumThread = new Thread(() => { maxNum = MaxNumThread(randomNumbers); });
            maxNumThread.Start();

            Thread minNumThread = new Thread(() => { minNum = MinNumThread(randomNumbers); });
            minNumThread.Start();

            Thread avgNumThread = new Thread(() => { avgNum = AvgNumThread(randomNumbers); });
            avgNumThread.Start();

            Thread printNumbersThread = new Thread(() => { PrintNumbersThread(randomNumbers); });
            printNumbersThread.Start();
            printNumbersThread.Join();

            /*Thread saveMaxNumThread = new Thread(() => { SaveToFileThread(maxNum, "Max num: "); });
            saveMaxNumThread.Start();
            saveMaxNumThread.Join();

            Thread saveMinNumThread = new Thread(() => { SaveToFileThread(minNum, "Min num: "); });
            saveMinNumThread.Start();
            saveMinNumThread.Join();

            Thread saveAvgNumThread = new Thread(() => { SaveToFileThread(avgNum, "Avg num: "); });
            saveAvgNumThread.Start();
            saveAvgNumThread.Join();*/

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));
                using (Stream stream = File.Create("Nums.xml"))
                {
                    xmlSerializer.Serialize(stream, "Max num: " + maxNum);
                    xmlSerializer.Serialize(stream, "Min num: " + minNum);
                    xmlSerializer.Serialize(stream, "Avg num: " + avgNum);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"Max Num: {maxNum}");
            Console.WriteLine($"Min Num: {minNum}");
            Console.WriteLine($"Avg Num: {avgNum}");
        }
    }
}
