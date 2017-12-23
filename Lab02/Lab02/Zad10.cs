using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab02
{
    class Zad10
    {
        private MatMulCalculator matMulCalculator = new MatMulCalculator();
        private static AutoResetEvent handle = new AutoResetEvent(false);

        public Zad10()
        {
            matMulCalculator.matMulCompleted += new MatMulCompletedEventHandler(
                MatMulCalculator_Completed);

            startAsync();

            handle.WaitOne();


        }

        private void startAsync()
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

            this.matMulCalculator.MatMulAsync(m1,m2,taskId);

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

            handle.Set();
        }
       
    }


}
