namespace Lesson_05_AsyncAwait_03
{
    internal class Program
    {
        static private async Task<int> FindMax(int[] arr)
        {
            int max = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    await Task.Delay(10);
                }
            }

            return max;
        }
        static private async Task<int> FindMin(int[] arr)
        {
            int min = arr[0];

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    await Task.Delay(10);
                }
            }

            return min;
        }
        static private async Task<double> FindAvg(int[] arr)
        {
            double avg = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                avg += arr[i];
                await Task.Delay(10);
            }

            return avg / arr.Length;
        }
        static private async Task<int> FindSum(int[] arr)
        {
            int sum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
                await Task.Delay(10);
            }

            return sum;
        }
        static private async Task PrintArr(int[] arr)
        {
            Console.Write("Arr: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
                await Task.Delay(10);
            }
            Console.WriteLine();
        }

        static async Task Main(string[] args)
        {
            int[] arr = { 10, 5, 8, 2, 7, 1, 6, 4, 9, 3 };

            var max = await FindMax(arr);
            var min = await FindMin(arr);
            var avg = await FindAvg(arr);
            var sum = await FindSum(arr);

            await PrintArr(arr);

            Console.WriteLine("Finding max num in arr...");
            await Task.Delay(1500);
            Console.WriteLine($"Max num in arr is {max}");

            Console.WriteLine("Finding min num in arr...");
            await Task.Delay(1500);
            Console.WriteLine($"Min num in arr is {min}");

            Console.WriteLine("Finding avg num of arr...");
            await Task.Delay(1500);
            Console.WriteLine($"Avg num of arr is {avg}");

            Console.WriteLine("Finding sum of arr...");
            await Task.Delay(1500);
            Console.WriteLine($"Sum of arr is {sum}");
        }
    }
}
