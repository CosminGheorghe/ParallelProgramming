using System.Diagnostics;

namespace ParallelProgramming
{
    class Program
    {
        #region Utility methods

        static long DoSomething()
        {
            long total = 0;
            for (int i = 1; i < 100000000; i++)
            {
                total += i;
            }
            return total;
        }

        static void Method1()
        {
            Thread.Sleep(200);
            Console.WriteLine($"Method 1 Completed by Thread={Thread.CurrentThread.ManagedThreadId}");
        }

        static void Method2()
        {
            Thread.Sleep(200);
            Console.WriteLine($"Method 2 Completed by Thread={Thread.CurrentThread.ManagedThreadId}");
        }

        static void Method3()
        {
            Thread.Sleep(200);
            Console.WriteLine($"Method 3 Completed by Thread={Thread.CurrentThread.ManagedThreadId}");
        }

        static void NormalAction()
        {
            Console.WriteLine($"Method type 1, Thread={Thread.CurrentThread.ManagedThreadId}");
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

        #region Invoke example
        
        static void SequentialCall()
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            
            Method1();
            Method2();
            Method3();
            stopWatch.Stop();

            Console.WriteLine($"Sequential Execution Took {stopWatch.ElapsedMilliseconds} Milliseconds");
        }

        static void ParallelInvoke()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            Parallel.Invoke(
                 Method1, Method2, Method3
            );
            
            stopWatch.Stop();
            Console.WriteLine($"Parallel Execution Took {stopWatch.ElapsedMilliseconds} Milliseconds");

        }

        static void DifferentTypes()
        {
            Parallel.Invoke(
                 NormalAction, // Invoking Normal Method
                 delegate ()   // Invoking an inline delegate 
                 {
                     Console.WriteLine($"Method type 2, Thread={Thread.CurrentThread.ManagedThreadId}");
                 },
                () =>   // Invoking a lambda expression
                {
                    Console.WriteLine($"Method type 3, Thread={Thread.CurrentThread.ManagedThreadId}");
                }
            );
        }

        #endregion

        static void Main(string[] args)
        {

            //StandardFor();
            //ParallelFor();
            //StandardForeach();
            //ParallelForeach();
            //SequentialCall();
            //ParallelInvoke();
            DifferentTypes();
        }
    }
}
