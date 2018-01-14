using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;

namespace Lab02
{
    public class MatMulCalculator
    {
        private delegate void MatMulCompleted(double[,] matrix1,double[,] matrix2, AsyncOperation asyncOperation);

        public event MatMulCompletedEventHandler matMulCompleted;

        public HybridDictionary HybridDictionary = new HybridDictionary();

        public SendOrPostCallback OnCompletedCallback;

        public MatMulCalculator()
        {
            OnCompletedCallback = new SendOrPostCallback(CalculateCompleted);
        }

        public void CalculateCompleted(object operationState)
        {
            MatMulCompletedEventArgs e = operationState as MatMulCompletedEventArgs;

            matMulCompleted?.Invoke(this, e);
        }

        private void Completion(double[,] mat1,double[,] mat2,double[,] result,
            Exception ex, bool canceled, AsyncOperation ao)
        {

            if (!canceled)
            {
                lock (HybridDictionary.SyncRoot)
                {
                    HybridDictionary.Remove(ao.UserSuppliedState);
                }
            }

            MatMulCompletedEventArgs e = 
                new MatMulCompletedEventArgs(
                    mat1,
                    mat2,
                    result,
                    ex,
                    canceled,
                    ao.UserSuppliedState);


            ao.PostOperationCompleted(OnCompletedCallback, e);
        }

        private bool TaskCancelled(object taskId)
        {
            lock (HybridDictionary.SyncRoot)
            {
                return(HybridDictionary[taskId] == null);
            }
        }

        private void CalculateWorker(double[,] mat1, double[,] mat2, AsyncOperation ao)
        {
            Exception e = null;
            double[,] result = null;

            if (!TaskCancelled(ao.UserSuppliedState))
            {
                try
                {
                    result = MatMul(mat1, mat2);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }

            this.Completion(
                mat1,
                mat2,
                result,
                e,
                TaskCancelled(ao.UserSuppliedState),
                ao);
        }

        double getVal(double[,] mat, int column, int row)
        {
            int size = mat.GetLength(0);

            if (column > size || row > size)
            {
                throw new ArgumentException();
            }

            return mat[column, row];
        }

        private double[,] MatMul(double[,] mat1, double[,] mat2)
        {
            if (mat1.GetLength(0) != mat2.GetLength(1) || mat1.GetLength(1) != mat2.GetLength(1) || mat1.GetLength(0) != mat1.GetLength(1))
            {
                throw new ArgumentException();
            }
            
            var size = mat1.GetLength(0);
            double[,] result = new double[size, size];

            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        sum += getVal(mat1,i,k) * getVal(mat2,k, j);
                    }
                    result[i, j] = sum;
                    sum = 0;
                }
            }

            return result;
        }

        public virtual void MatMulAsync(double[,] mat1, double[,] mat2, object taskID)
        {
            AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(taskID);

            lock (HybridDictionary.SyncRoot)
            {
                if (HybridDictionary.Contains(taskID))
                {
                    throw new ArgumentException("TaskID parameter must be unique", "TaskID");
                }
            }

            HybridDictionary[taskID] = asyncOperation;

            MatMulCompleted workerDelegate = new MatMulCompleted(CalculateWorker);
            workerDelegate.BeginInvoke(
                mat1,
                mat2,
                asyncOperation,
                null,
                null);
        }

        public void CancelAsync(object taskId)
        {
            if (HybridDictionary[taskId] is AsyncOperation asyncOperation)
            {
                lock (HybridDictionary.SyncRoot)
                {
                    HybridDictionary.Remove(taskId);
                }
            }
        }
    }

    class Zad9
    {
        public Zad9()
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
            MatMulCalculator calc = new MatMulCalculator();

            calc.MatMulAsync(m1,m2,new object());

            Console.WriteLine("Obliczono macierz");
        }  
    }
}
