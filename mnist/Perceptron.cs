using mnist.VectorMath;
using System;

namespace mnist
{
    public class Perceptron
    {
        static Function ActivFunction = x => 1 / (1 + Math.Exp(-x));
        static Function DerivativeActivFunction = x => (1 - ActivFunction(x))*ActivFunction(x);
        static VectorFunction VectorActivFunction = Vector.ToVectorFunction(ActivFunction);
        static VectorFunction VectorDerivativeActivFunction = Vector.ToVectorFunction(DerivativeActivFunction);

        static double LearningRate = 0.1;
        const int inputLen = 28 * 28;
        const int outputLen = 10;

        int[] Structure = { 28 * 28, 10 };
        Layer layer;
        public Perceptron()
        {
            layer = new Layer(inputLen, outputLen);
        }
        public struct Layer
        {
            public Layer(int len1, int len2)
            {
                weigths = new Matrix(len2, len1);
                shift = new Vector(len2);
            }
            public Matrix weigths;
            public Vector shift;
        }

        public Vector input { get; set; } = new Vector(inputLen);
        public Vector output { get; private set; } = new Vector(outputLen);

        private const int SEED = 17112022;
        readonly Random random = new Random(SEED);
        public void RandomFill()
        {
            layer.weigths = random.RandomMatrix(layer.weigths.GetSize());
            layer.shift = random.RandomVector(layer.shift.GetLength());
        }
        public Vector Forward(Vector input)
        {
            this.input = input;
            output = VectorActivFunction(layer.weigths * input + layer.shift);
            return output;
        }
        public void Backward(Vector expected)
        {
            Vector error = Vector.HadamardMultiply(output - expected, VectorDerivativeActivFunction(layer.weigths * input + layer.shift));
            layer.shift -= LearningRate * error;
            layer.weigths -= LearningRate * (Matrix)error * Matrix.Transporate((Matrix)input);
        }
    }
}
