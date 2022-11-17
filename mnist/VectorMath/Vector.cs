using System;

namespace mnist.VectorMath
{
    public class Vector
    {
        public Vector(params double[] data)
        {
            this.data = data;
        }
        public Vector(int length)
        {
            data = new double[length];
        }
        internal double[] data;
        public int GetLength()
        {
            return data.Length;
        }
        public double GetEuclideanLength()
        {
            double length = 0;
            for (int i=0; i<data.Length; i++)
                length += data[i] * data[i];
            return Math.Sqrt(length);
        }
        public double[] ToArray()
        {
            return data;
        }
        public static VectorFunction ToVectorFunction(Function func)
        {
            return v =>
            {
                double[] result = new double[v.GetLength()];
                for (int i = 0; i < v.GetLength(); i++)
                    result[i] = func(v.data[i]);
                return new Vector(result);
            };
        }

        public static explicit operator Matrix(Vector v)
        {
            double[,] data = new double[v.GetLength(), 1];
            for (int i=0; i<v.GetLength(); i++)
            {
                data[i, 0] = v.data[i];
            }
            return new Matrix(data);
        }
        public static Vector operator -(Vector v)
        {
            double[] data = new double[v.GetLength()];
            for (int i = 0; i < v.GetLength(); i++)
                data[i] = -v.data[i];
            return new Vector(data);
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1.GetLength() != v2.GetLength())
                throw new Exception("Vectors have incorrect length");
            double[] data = new double[v1.GetLength()];
            for (int i = 0; i < v1.GetLength(); i++)
                data[i] = v1.data[i] - v2.data[i];
            return new Vector(data);
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1.GetLength() != v2.GetLength())
                throw new Exception("Vectors have incorrect length");
            double[] data = new double[v1.GetLength()];
            for (int i=0; i<v1.GetLength(); i++)
                data[i] = v1.data[i] + v2.data[i];
            return new Vector(data);
        }
        public static Vector operator *(double k, Vector v)
        {
            double[] data = new double[v.GetLength()];
            for (int i = 0; i < v.GetLength(); i++)
                data[i] = k * v.data[i];
            return new Vector(data);
        }
        public static Vector operator *(Vector v, double k)
        {
            return k * v;
        }
        public static Vector operator *(Matrix A, Vector v)
        {
            if (A.GetLength(1) != v.GetLength())
                throw new Exception("Matrix and vector have incorrect size");
            double[] data = new double[A.GetLength(0)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    data[i] = A.data[i, j] * v.data[j];
            return new Vector(data);
        }

        public static Vector HadamardMultiply(Vector v1, Vector v2)
        {
            if (v1.GetLength() != v2.GetLength())
                throw new Exception("Vectors have incorrect length");
            double[] data = new double[v1.GetLength()];
            for (int i = 0; i < v1.GetLength(); i++)
                data[i] = v1.data[i] * v2.data[i];
            return new Vector(data);
        }
    }
}
