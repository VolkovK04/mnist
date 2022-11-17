using mnist.VectorMath;
using System;

namespace mnist
{
    internal static class RandomExtension
    {
        public static Vector RandomVector(this Random rand, int len)
        {
            double[] data = new double[len];
            for (int i = 0; i < len; i++)
            {
                data[i] = rand.NextDouble();
            }
            return new Vector(data);
        }
        public static Matrix RandomMatrix(this Random rand, int weigth, int height)
        {
            double[,] data = new double[weigth, height];
            for (int i = 0; i < weigth; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    data[i, j] = rand.NextDouble();
                }
            }
            return new Matrix(data);
        }
        public static Matrix RandomMatrix(this Random rand, Size size)
        {
            return rand.RandomMatrix(size.Width, size.Height);
        }

    }
}
