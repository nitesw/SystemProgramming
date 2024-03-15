namespace Lesson_05_AsyncAwait_02
{
    internal class Program
    {
        static private async Task ShowPrimeNums()
        {
            Console.WriteLine("Starting to print all prime nums...");
            await Task.Delay(3000);

            for (int i = 0; i <= 1000; i++)
            {
                if (IsPrime(i))
                {
                    Console.Write(i + " ");
                    await Task.Delay(10);
                }
            }

            Console.WriteLine("\nFinishing printing all prime nums...");
            await Task.Delay(3000);
        }
        static private async Task<int> ShowRangedPrimeNums(int num1, int num2)
        {
            int count = 0;

            Console.WriteLine($"\nStarting to count all prime nums from {num1} to {num2}...\n");
            await Task.Delay(3000);
            for (int i = num1; i < num2; i++)
            {
                if (IsPrime(i))
                {
                    count++;
                    await Task.Delay(10);
                }
            }

            return count;
        }

        static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        static async Task Main(string[] args)
        {
            var startTime = DateTime.Now;
            int n1, n2;

            //await ShowPrimeNums();
            Console.Write("Enter first num: ");
            n1 = int.Parse(Console.ReadLine());
            Console.Write("Enter second num: ");
            n2 = int.Parse(Console.ReadLine());

            if(n1 > n2)
            {
                int tmp = n1;
                n1 = n2;
                n2 = tmp;
            }

            int primeCount = await ShowRangedPrimeNums(n1, n2);
            Console.WriteLine($"Total count of prime nums in range of {n1} and {n2} is {primeCount}");

            var endTime = DateTime.Now;
            Console.WriteLine($"\nTotal time: " + (endTime - startTime) + " minutes");
        }
    }
}
