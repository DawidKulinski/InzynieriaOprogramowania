using System;
using System.ComponentModel;
using System.Windows;

namespace Lab02
{
    class CompletedEventArgs : AsyncCompletedEventArgs
    {
        public int size;

        private int[,] matrix1;

        public int[,] Matrix1
        {
            get
            {
                //RaiseExceptionIfNecessary();
                return matrix1;
            }
        }

        private int[,] matrix2;

        public int[,] Matrix2
        {
            get
            {
                //RaiseExceptionIfNecessary();
                return matrix2;
            }
        }

        private int[,] matrix3;

        public int[,] Matrix3
        {
            get
            {
                //RaiseExceptionIfNecessary();
                return matrix3;
            }
            set { matrix3 = value; }
        }

        public CompletedEventArgs(int size, int[,] arg1, int[,] arg2, Exception e, bool cancelled, object state)
            : base(e, cancelled, state)
        {
            this.size = size;
            this.matrix1 = arg1;
            this.matrix2 = arg2;
        }
    }
}