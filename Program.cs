using System;
using System.Threading;

public class Work
{
    public static int n;
    public static int counter = 1;
    const int limit = 10;
    static object monitor = new object();
    public static void Main()
    {
        //// Start a thread that calls a parameterized static method.
        //Thread newThread = new Thread(Work.DoWork);
        //newThread.Start(42);

        //// Start a thread that calls a parameterized instance method.
        //Work w = new Work();
        //newThread = new Thread(w.DoMoreWork);
        //newThread.Start("Qhonthol.");
        Thread oddThread = new Thread(Odd);
        Thread evenThread = new Thread(Even);
        n = 10;
        oddThread.Start();
        Thread.Sleep(1000);
        evenThread.Start();

        evenThread.Join();
        oddThread.Join();
        Console.WriteLine("Done");
        Console.ReadLine();
      

       
    }

    public static void Odd()
    {
        try
        {
            //hold lock
            Monitor.Enter(monitor);
            for (int i = 1; i <= limit; i = i + 2)
            {
                //Complete the task ( printing even number on console)
                Console.WriteLine("Thread #1 " + i);
                //Notify other thread- here odd thread
                //that I'm done, you do your job
                Monitor.Pulse(monitor);
                //I will wait here till odd thread notify me
                // Monitor.Wait(monitor);
                Thread.Sleep(1000);
                bool isLast = i == limit;
                if (!isLast)
                    Monitor.Wait(monitor);
            }
        }
        finally
        {
            Monitor.Exit(monitor);//release the lock
        }

        //for(int i=0;i<5;i++)
        //      if(counter%2==0)
        //  Console.WriteLine("Thread #1",
        //      counter);
        //  Console.ReadKey();

    }

    public static void Even()
    {
        try
        {
            //hold lock as console is shared between threads.
            Monitor.Enter(monitor);
            for (int i = 2; i <= limit; i = i + 2)
            {
                //Complete the task ( printing odd number on console)
                Console.WriteLine("Thread #2 " + i);
                //Notify other thread that is to eventhread
                //that I'm done you do your job
                Monitor.Pulse(monitor);

                //I will wait here till even thread notify me
                // Monitor.Wait(monitor);
                Thread.Sleep(2500);
                // without this logic application will wait forever
                bool isLast = i == limit - 1;
                if (!isLast)
                    Monitor.Wait(monitor); //I will wait here till even thread notify me
            }
        }
        finally
        {
            //Release lock
            Monitor.Exit(monitor);
        }



        //for (int i = 0; i < 5; i++)
        //    if (counter % 2 == 1)
        //        Console.WriteLine("Thread #2",
        //            counter);
        //Console.ReadKey();
    }
}