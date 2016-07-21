using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.Threads.Basic
{
    class Alpha
    {
        // this method will be called when thread starts
        public void Beta()
        {
            while (true)
            {
                Console.WriteLine("Alpha.Beta is running on this thread");
            }
        }
    };

    class Program
    {
        static void StartStopJoin()
        {
            Console.WriteLine("Thread Start/Stop/Join Sample");

            Alpha oAlpha = new Alpha();

            // create the thread but not start it
            Thread oThread = new Thread(new ThreadStart(oAlpha.Beta));

            // start the thread
            oThread.Start();

            while (!oThread.IsAlive) ; // wait until thread starts

            Thread.Sleep(1000); // put the main thread to sleep, to allow oThread to do some work.

            // request oThread to stop
            oThread.Abort();

            // Wait until oThread finishes. Join also has overloads
            // that take a millisecond interval or a TimeSpan object.
            oThread.Join();

            Console.WriteLine();
            Console.WriteLine("Alpha.Beta has finished");

            try
            {
                Console.WriteLine("Try to restart the Alpha.Beta thread.");
                oThread.Start();
            }
            catch (ThreadStateException)
            {
                Console.Write("ThreadStateException trying to restart Alpha.Beta. ");
                Console.WriteLine("Expected since aborted threads cannot be restarted.");
            }
            finally
            {
                Thread.Sleep(5000); // sleep for 5 sec before ends.
            }
        }

        static void Main(string[] args)
        {
            StartStopJoin();
        }
    }
}
