using System;
using System.Diagnostics;

namespace Lab02
{


    public class Zad8
    {
        delegate int DelegateType(int arguments);

        public static object Locker = new object();

        private static DelegateType FacIter1 { get; set; }

        //Obliczanie silni za pomoca metody rekurencynej.
        public int FacRec(int arg)
        {
            if (arg == 0 || arg == 1)
                return 1;
            return arg * FacRec(arg - 1);
        }
        //Obliczanie silni za pomocą metody iteracyjnej.
        public int FacIter(int arg)
        {
            var result = arg;
            for (arg = arg-1; arg > 0; arg--)
            {
                result *= arg;
            }
            return result;
        }
        //Obliczanie ciągu fibonacciego za pomocą metody rekurencyjnej.
        public int FibRec(int arg)
        {
            if (arg == 0)
                return 0;
            if (arg == 1)
                return 1;
            return FibRec(arg - 1) + FibRec(arg - 2);
        }
        //Obliczanie ciągu fibonacciego za pomocą metody iteracyjnej
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
            var facRec = new DelegateType(FacRec);
            var fibIter = new DelegateType(FibIter);
            var fibRec = new DelegateType(FibRec);
            FacIter1 = FacIter;


            const int s = 35;
            var facRecResult = StopwatchUtil.Time(() =>
            {
                var result1 = facRec.BeginInvoke(s, null, null);
                facRec.EndInvoke(result1);
            });

            Console.WriteLine($"Czas obliczenia silni rekurencyjnie: {facRecResult}");

            var facIterResult = StopwatchUtil.Time(() =>
            {
                var result1 = FacIter1.BeginInvoke(s, null, null);
                FacIter1.EndInvoke(result1);
            });

            Console.WriteLine($"Czas obliczenia silni iteracyjnie {facIterResult}");

            var fibRecResult = StopwatchUtil.Time(() =>
            {
                var result1 = fibRec.BeginInvoke(s, null, null);
                fibRec.EndInvoke(result1);
            });

           Console.WriteLine($"Czas obliczenia ciągu fibonacciego rekurencyjnie {fibRecResult}");

            var fibIterResult = StopwatchUtil.Time(() =>
            {
                var result1 = fibIter.BeginInvoke(s, null, null);
                fibIter.EndInvoke(result1);
            });

            Console.WriteLine($"Czas obliczenia ciągu fibonacciego iteracyjnie {fibIterResult}");
        }
    }
    public static class StopwatchUtil
    {
        //Funkcja pomocnicza do mierzenia czasu wykonywania danego fragmentu programu.
        public static long Time(Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
