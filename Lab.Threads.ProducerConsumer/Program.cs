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
        }
    }
}
