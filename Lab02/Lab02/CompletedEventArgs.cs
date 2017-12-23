using System;
using System.ComponentModel;
using System.Windows;

namespace Lab02
{
    public delegate void MatMulCompletedEventHandler(object sender, MatMulCompletedEventArgs e);

    public class MatMulCompletedEventArgs : AsyncCompletedEventArgs
    {
        public double[,] Matrix1 { get;}

        public double[,] Matrix2 { get;}

        public double[,] Result { get; }

        public MatMulCompletedEventArgs(double[,] mat1,double[,] mat2,double[,] result,Exception e, bool cancelled, object state)
            : base(e, cancelled, state)
        {
            Matrix1 = mat1;
            Matrix2 = mat2;
            Result = result;
        }
    }
}