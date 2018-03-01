using System;
using System.Linq;
using System.Threading;

namespace AsParallelDeadlock
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Execute(new ManualResetEventSlim());
        }

        static void Execute(ManualResetEventSlim mre)
        {
            Console.WriteLine(System.Diagnostics.Process.GetCurrentProcess().Id);
            Enumerable.Range(0, Environment.ProcessorCount*100).AsParallel()
                .ForAll(j =>
                {
                    if (j == Environment.ProcessorCount)
                    {
                        Console.WriteLine("Set on {0} with value of {1}", Thread.CurrentThread.ManagedThreadId, j);
                        mre.Set();
                    }
                    else
                    {
                        Console.WriteLine("Waiting on {0} with value of {1}", Thread.CurrentThread.ManagedThreadId, j);
                        mre.Wait();
                    }
                });
        }
    }
}