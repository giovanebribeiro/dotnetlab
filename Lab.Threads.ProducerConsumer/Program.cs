using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.Threads.ProducerConsumer
{
    public class Cell
    {
        int cellContents; //cell contents
        bool readerFlag = false; // state flag

        public int readFromCell() // the 'consumer' method
        {
            lock (this) // enter synchronization block
            {
                if (!readerFlag)
                {
                    try
                    {
                        Monitor.Wait(this); //waits for Monitor.Pulse
                    }
                    catch (SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch(ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                }
                Console.WriteLine("Consume: {0}", cellContents);
                readerFlag = false; // reset the state flag to say consuming is done.
                Monitor.Pulse(this); // Pulse tells Cell.WriteToCell that Cell.readFromCell is done.
            } // end sync block
            return cellContents;
        }

        public void WriteToCell(int n) // the 'producer' method
        {
            lock (this) // Enter sync block
            {
                if (readerFlag) // wait until Cell.ReadFromCell is done consuming
                {
                    try
                    {
                        Monitor.Wait(this); // Wait for the Monitor.Pulse in ReadFromCell
                    }
                    catch (SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch(ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                }
                cellContents = n;
                Console.WriteLine("Produce: {0}", cellContents);
                readerFlag = true; // set the state flag to say that producing is done
                Monitor.Pulse(this); // Pulse tells Cell.readFromCell that Cell.WriteToCell is done.
            } // end sync block
        }
    };

    public class CellCons
    {
        Cell cell; 
        int quantity = 1;

        public CellCons(Cell box, int request)
        {
            cell = box;
            quantity = request;
        }

        public void ThreadRun()
        {
            int valReturned;
            for(int looper = 1; looper<= quantity; looper++)
            {
                // consume the result
                valReturned = cell.readFromCell();
            }
        }
    };

    public class CellProd
    {
        Cell cell;
        int quantity = 1;

        public CellProd(Cell box, int request)
        {
            cell = box;
            quantity = request;
        }

        public void ThreadRun()
        {
            for (int looper = 1; looper <= quantity; looper++)
            {
                // consume the result
                cell.WriteToCell(looper); //producing
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            Cell cell = new Cell();
            CellProd prod = new CellProd(cell, 20); // produce 20 items
            CellCons cons = new CellCons(cell, 20); // consumes 20 items

            Thread producer = new Thread(new ThreadStart(prod.ThreadRun));
            Thread consumer = new Thread(new ThreadStart(cons.ThreadRun));

            try
            {
                producer.Start();
                consumer.Start();

                producer.Join();
                consumer.Join();

                Thread.Sleep(10000); //sleep for 10 seconds, to allow us to see the results
            }
            catch (ThreadStateException e)
            {
                Console.WriteLine(e);
                result = 1;
            }
            catch(ThreadInterruptedException e)
            {
                Console.WriteLine(e);
                result = 1;
            }



            Environment.ExitCode = result;
        }
    }
}
