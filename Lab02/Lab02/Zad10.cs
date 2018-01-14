using System;
using System.Threading;

namespace Lab02
{
    class Zad10
    {
        private readonly MatMulCalculator _matMulCalculator = new MatMulCalculator();
        private static readonly AutoResetEvent Handle = new AutoResetEvent(false);

        public Zad10()
        {
            _matMulCalculator.matMulCompleted += new MatMulCompletedEventHandler(
                MatMulCalculator_Completed);

            StartAsync();

            Handle.WaitOne();


        }

        private void StartAsync()
        {
            var m1 = new double[,]
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };
            var m2 = new double[,]
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };

            Guid taskId = Guid.NewGuid();

            this._matMulCalculator.MatMulAsync(m1,m2,taskId);

        }

        private static void MatMulCalculator_Completed(
            object sender,
            MatMulCompletedEventArgs e)
        {
            Guid taskId = (Guid) e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("Operation Canceled");
            }
            else if (e.Error != null)
            {
                Console.WriteLine("Error");
            }
            else
            {
                double[,] result = e.Result;

                for (int i = 0; i < result.GetLength(0); i++)
                {
                    for (int k = 0; k < result.GetLength(1); k++)
                    {
                        Console.Write(result[i, k]+" ");
                    }

                    Console.WriteLine();
                }
            }

            Handle.Set();
        }
       
    }


}
