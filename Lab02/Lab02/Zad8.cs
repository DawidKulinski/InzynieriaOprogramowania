using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{


    public class Zad8
    {
        delegate int DelegateType(int arguments);
        private static DelegateType facRec;
        private static DelegateType facIter;
        private static DelegateType fibRec;
        private static DelegateType fibIter;

        public static object locker = new object();

        public int FacRec(int arg)
        {
            if (arg == 0 || arg == 1)
                return 1;
            return arg * FacRec(arg - 1);
        }

        public int FacIter(int arg)
        {
            var result = arg;
            for (arg = arg-1; arg > 0; arg--)
            {
                result *= arg;
            }
            return result;
        }

        public int FibRec(int arg)
        {
            if (arg == 0)
                return 0;
            if (arg == 1)
                return 1;
            return FibRec(arg - 1) + FibRec(arg - 2);
        }

        public int FibIter(int arg)
        {
            if (arg == 0) return 0;
            if (arg == 1) return 1;

            var l1 = 0;
            var l2 = 1;
            var l3 = 0;

            for (int i = 2; i <= arg; i++)
            {
                l3 = l1 + l2;
                l1 = l2;
                l2 = l3;
            }
            return l3;
        }

        public Zad8()
        {
            facRec = new DelegateType(FacRec);
            facIter = new DelegateType(FacIter);
            fibIter = new DelegateType(FibIter);
            fibRec = new DelegateType(FibRec);


            int s = 4;
            var facRecResult = StopwatchUtil.Time(() =>
            {
                var result1 = facRec.BeginInvoke(s, null, null);
                facRec.EndInvoke(result1);
            });

            Console.WriteLine(facRecResult);

            var facIterResult = StopwatchUtil.Time(() =>
            {
                var result1 = facIter.BeginInvoke(s, null, null);
                facIter.EndInvoke(result1);
            });

            Console.WriteLine(facIterResult);

            var fibRecResult = StopwatchUtil.Time(() =>
            {
                var result1 = fibRec.BeginInvoke(s, null, null);
                fibRec.EndInvoke(result1);
            });

           Console.WriteLine(fibRecResult);

            var fibIterResult = StopwatchUtil.Time(() =>
            {
                var result1 = fibIter.BeginInvoke(s, null, null);
                fibIter.EndInvoke(result1);
            });

            Console.WriteLine(fibIterResult);
        }
    }
    public static class StopwatchUtil
    {
        public static long Time(Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
