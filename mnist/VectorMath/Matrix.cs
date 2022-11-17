using System;

namespace mnist.VectorMath
{
    public class Matrix
    {
        public Matrix(double[,] data)
        {
            this.data = data;
        }
        public Matrix(int width, int height)
        {
            data = new double[width, height];
        }

        internal double[,] data;

        public static Matrix Diagonal(params double[] data)
        {
            double[,] result = new double[data.Length, data.Length];
            for (int i = 0; i < data.Length; i++)
                result[i, i] = data[i];
            return new Matrix(result);
        }
        public int GetLength(int dim)
        {
            return data.GetLength(dim);
        }
        public Size GetSize()
        {
            return new Size(data.GetLength(0), data.GetLength(1));
        }
        public static Matrix Transporate(Matrix A)
        {
            double[,] data = new double[A.GetLength(1), A.GetLength(0)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    data[j, i] = A.data[i, j];
            return new Matrix(data);
        }
        public static Matrix operator -(Matrix A)
        {
            double[,] data = new double[A.GetLength(0), A.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    data[i, j] = -A.data[i, j];
            return new Matrix(data);
        }
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A.GetSize() != B.GetSize())
                throw new Exception("Matrix have incorrect size");
            double[,] data = new double[A.GetLength(0), A.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    data[i, j] = A.data[i, j] - B.data[i, j];
            return new Matrix(data);
        }
        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A.GetSize() != B.GetSize())
                throw new Exception("Matrix have incorrect size");
            double[,] data = new double[A.GetLength(0), A.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j=0; j<A.GetLength(1); j++)
                    data[i, j] = A.data[i, j] + B.data[i, j];
            return new Matrix(data);
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.GetLength(1) != B.GetLength(0))
                throw new Exception("Matrix have incorrect size");
            double[,] data = new double[A.GetLength(0), B.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    for (int k = 0; k < B.GetLength(1); k++)
                        data[i, k] = A.data[i, j] * B.data[j, k];
            return new Matrix(data);
        }
        public static Matrix operator *(Matrix A, double k)
        {
            double[,] data = new double[A.GetLength(0), A.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    data[i, j] = k*A.data[i, j];
            return new Matrix(data);
        }
        public static Matrix operator *(double k, Matrix A)
        {
            return A * k;
        }

    }
}
