using System;
using System.Threading;

public class Work
{
   // public static int n;
   // public static int counter = 1;
    const int limit = 10;
    static object monitor = new object();
    public static void Main()
    {
        //Deklarasi Thread
        Thread oddThread = new Thread(Odd);
        Thread evenThread = new Thread(Even);
        //n = 10;
        oddThread.Start();
        //Memberi delay pada Thread
        Thread.Sleep(1000);
        evenThread.Start();

        evenThread.Join();
        oddThread.Join();
        Console.WriteLine("Done");
        Console.ReadKey();



    }

    public static void Odd()
    {
        try
        {
            //kunci monitor
            Monitor.Enter(monitor);
            for (int i = 1; i <= limit; i = i + 2)
            {
  
                Console.WriteLine("Thread #1 " + i);

                Monitor.Pulse(monitor);
            
                Thread.Sleep(1000);
                bool isLast = i == limit;
                if (isLast= true)
                    Monitor.Wait(monitor);
            }
        }
        finally
        {
            Monitor.Exit(monitor);//lepas kuncian monitor yang diatas
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
            
            Monitor.Enter(monitor);
            for (int i = 2; i <= limit; i = i + 2)
            {
                
                Console.WriteLine("Thread #2 " + i);
                
                Monitor.Pulse(monitor);

               
                Thread.Sleep(2500);
                
                bool isLast = i == limit - 1;
                if (isLast = true)
                    Monitor.Wait(monitor); 
            }
        }
        finally
        {
            Monitor.Exit(monitor);
        }




    }
}
