using System;
using System.Threading;

class SimpleDeadlock
{

    static SimpleDeadlock lockA = new SimpleDeadlock();
    static object lockB = new object();

    static void normal_order()
    {
        lock (lockA)
        {
            Console.WriteLine("lock A");
            Thread.Sleep(500);
            lock (lockB)
            {
                Console.WriteLine("lock B");
            }
        }
    }

    static void reverse_order()
    {
        lock (lockB)
        {
            Console.WriteLine("lock B");
            Thread.Sleep(500);
            lock (lockA)
            {
                Console.WriteLine("lock A");
            }
        }
    }

    static void Main()
    {
        Console.WriteLine(System.Diagnostics.Process.GetCurrentProcess().Id);
        Thread t1 = new Thread(
            new ThreadStart(normal_order));
        Thread t2 = new Thread(
            new ThreadStart(reverse_order));
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
    }

}