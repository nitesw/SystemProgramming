namespace Lesson_05_AsyncAwait_01
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var startTime = DateTime.Now;
            var taskEgg = FryEgg(3);
            var taskBeacon = FryBeacon(5);
            var taskToast = ToastBread(2);

            Coffee cup = PourCoffee();
            Console.WriteLine("Coffee is ready.");

            Egg egg = await taskEgg;
            Console.WriteLine("Eggs are ready.");

            Beacon beacon = await taskBeacon;
            Console.WriteLine("Beacon are ready.");

            Toast toast = await taskToast;
            Console.WriteLine("Toasts are ready.");

            var endTime = DateTime.Now;
            Console.WriteLine($"Total time: " + (endTime - startTime) + " minutes");
        }

        private static async Task<Toast> ToastBread(int slices)
        {
            for (int i = 0; i < slices; i++)
            {
                Console.WriteLine($"Putting slices to toaster.");
            }

            Console.WriteLine("Start toasting...");
            await Task.Delay(5000);
            Console.WriteLine("Remove from toaster...");
            return new Toast();
        }

        private static async Task<Beacon> FryBeacon(int slices)
        {
            Console.WriteLine($"Putting {slices} slice of beacon in the pan.");
            Console.WriteLine("Cooking beacon.");
            await Task.Delay(3000);
            for (int i = 0; i < slices; i++)
            {
                Console.WriteLine($"Flipping slices of beacon.");
            }
            Console.WriteLine("Cooking slices...");
            await Task.Delay(3000);
            Console.WriteLine("Put beacon on plate.");

            return new Beacon();
        }

        private static async Task<Egg> FryEgg(int count)
        {
            Console.WriteLine($"Count of eggs is {count}");
            Console.WriteLine("Cooking eggs.");
            await Task.Delay(6000);
            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }

    }
}
