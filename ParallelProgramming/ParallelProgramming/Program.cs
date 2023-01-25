using System.Diagnostics;

namespace ParallelProgramming
{
    class Program
    {
        #region Utility method

        static long DoSomething()
        {
            long total = 0;
            for (int i = 1; i < 100000000; i++)
            {
                total += i;
            }
            return total;
        }

        #endregion

        #region Standard vs Parallel for 

        static void StandardFor()
        {
            Stopwatch stopWatch = new Stopwatch();

            Console.WriteLine("For Loop Execution start");

            stopWatch.Start();
            for (int i = 0; i < 10; i++)
            {
                long total = DoSomething();
                Console.WriteLine("{0} - {1}", i, total);
            }

            Console.WriteLine("For Loop Execution end ");
            stopWatch.Stop();
            Console.WriteLine($"Time Taken to Execute the For Loop in miliseconds {stopWatch.ElapsedMilliseconds}");
        }

        static void ParallelFor()
        {
            Stopwatch stopWatch = new Stopwatch();

            Console.WriteLine("Parallel For Loop Execution start");

            stopWatch.Start();
            Parallel.For(0, 10, i => {
                long total = DoSomething();
                Console.WriteLine("{0} - {1}", i, total);
            });

            Console.WriteLine("Parallel For Loop Execution end ");
            stopWatch.Stop();
            Console.WriteLine($"Time Taken to Execute Parallel For Loop in miliseconds {stopWatch.ElapsedMilliseconds}");
        }

        #endregion

        #region Standard vs Parallel foreach

        static void StandardForeach()
        {
            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Standard Foreach Loop Started");

            stopwatch.Start();
            List<int> integerList = Enumerable.Range(1, 10).ToList();

            foreach (int i in integerList)
            {
                long total = DoSomething();
                Console.WriteLine("{0} - {1}", i, total);
            };

            Console.WriteLine("Standard Foreach Loop Ended");
            stopwatch.Stop();
            Console.WriteLine($"Time Taken by Standard Foreach Loop in Miliseconds {stopwatch.ElapsedMilliseconds}");
        }

        static void ParallelForeach()
        {
            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Parallel Foreach Loop Started");

            stopwatch.Start();
            List<int> integerList = Enumerable.Range(1, 10).ToList();

            Parallel.ForEach(integerList, i =>
            {
                long total = DoSomething();
                Console.WriteLine("{0} - {1}", i, total);
            });

            Console.WriteLine("Parallel Foreach Loop Ended");
            stopwatch.Stop();
            Console.WriteLine($"Time Taken by Parallel Foreach Loop in Miliseconds {stopwatch.ElapsedMilliseconds}");
        }

        #endregion

        static void Main(string[] args)
        {

            //StandardFor();
            //ParallelFor();
            //StandardForeach();
            ParallelForeach();
        }
    }
}
