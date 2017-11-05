using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO
{
    public class Zad1
    {
        public Zad1()
        {

            ThreadPool.QueueUserWorkItem(ThreadProc, 123);
            ThreadPool.QueueUserWorkItem(ThreadProc, 456);
            ThreadPool.QueueUserWorkItem(ThreadProc, 900);

            Thread.Sleep(2000);
        }

        static void ThreadProc(Object stateInfo)
        {

            Thread.Sleep((int)stateInfo);
            Console.WriteLine("Hello");

            Console.WriteLine("Poczekałem "+(int)stateInfo+" sekund");

        }
    }
}
