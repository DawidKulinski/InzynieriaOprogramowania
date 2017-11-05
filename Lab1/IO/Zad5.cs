using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO
{
    class Zad5
    {
        private static long _sum;
        private static object _lock = new object();

        public Zad5(int arrSize=10000, int partSize=1000)
        {
            _sum = 0;
            var table = _generateArr(arrSize);
            var splitted = table.SplitArrayBy(partSize).ToArray();
            var handles = new WaitHandle[splitted.Length];
            Console.WriteLine(table.Sum());

            for (var i=0;i<splitted.Length; i++)
            {
                var handle = new AutoResetEvent(false);
                ThreadPool.QueueUserWorkItem(Sum, new object[] {splitted[i],handle});
                handles[i] = handle;
            }
            WaitHandle.WaitAll(handles);

            Console.WriteLine(_sum);
        }

        public static void Sum(object table)
        {
            var tab = (object[]) table;

            var array = (int[]) tab[0];
            var handle = (AutoResetEvent) tab[1];

            var sum = array.Sum();

            lock (_lock)
            {
                _sum += sum;
            }
            handle.Set();
        }

        private int[] _generateArr(int arrSize)
        {
            var rnd = new Random();
            var table = new int[arrSize];

            for (int i = 0; i < arrSize; i++)
            {
                table[i] = rnd.Next(1, 50);
            }

            return table;
        }

    }
    public static class EnumerableEx
    {
        public static IEnumerable<int[]> SplitArrayBy(this int[] arr, int chunkLength)
        {
            if (arr == null) throw new ArgumentException();
            if (chunkLength < 1) throw new ArgumentException();

            for (var i = 0; i < arr.Length; i += chunkLength)
            {
                yield return i + chunkLength > arr.Length ?
                    arr.Skip(i).Take(arr.Length - i).ToArray() :
                    arr.Skip(i).Take(chunkLength).ToArray();
            }
        }
    }
}
