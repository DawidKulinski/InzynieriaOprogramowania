using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    public class MatMulCalculator
    {
      /*  delegate void MatMulCompleted(CompletedEventArgs args);

        private static event MatMulCompleted foo;

        public HybridDictionary dictionary;

        public delegate void onCompletedCallback(object state);

        static void CalculateCompleted(CompletedEventArgs args)
        {
            foo.Invoke(new CompletedEventArgs());
        }

        public MatMulCalculator()
        {
            onCompletedCallback = CalculateCompleted();
        }
        */
    }

    class Zad9
    {

/*
        static void CalculateCompleted

        static void ComputeMatrix(CompletedEventArgs args)
        {
            var matrix1 = args.Matrix1;
            var matrix2 = args.Matrix2;
            int[,] matrix3 = new int[args.size, args.size];

            int sum = 0;
            for (int i = 0; i < args.size; i++)
            {
                for (int j = 0; j < args.size; j++)
                {
                    for (int k = 0; k < args.size; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    matrix3[i, j] = sum;
                    sum = 0;
                }
            }

            args.Matrix3 = matrix3;
        }
        public Zad9()
        {
            foo = ComputeMatrix;
            foo += onCompletedCallback;
            foo.Invoke(new CompletedEventArgs(3,new int[,]{ {1,2,3}, {4,5,6}, {7,8,9} }, 
                new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } },
                new Exception(),
                false,
                new object()));
        }

        static void onCompletedCallback(CompletedEventArgs args)
        {
            for (int i = 0; i < args.size; i++)
            {
                for (int j = 0; j < args.size; j++)
                {
                    Console.Write(args.Matrix3[i,j]+" ");
                }
                Console.WriteLine();
            }
        } */
    }
}
