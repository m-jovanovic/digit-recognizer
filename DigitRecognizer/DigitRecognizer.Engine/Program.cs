using System;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Data;
using DigitRecognizer.MachineLearning.Functions;
using DigitRecognizer.MachineLearning.Optimizers;

namespace DigitRecognizer.Engine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var m1 = VectorUtilities.CreateMatrix(50, 75);

            //var m23 = new double [][]
            //{
            //    new double [] {1.0, 2.0, 3.0, 4.0, 5.0},
            //    new double[] {1.0, 2.0, 3.0, 4.0, 5.0},
            //    new double[] {1.0, 2.0, 3.0, 4.0, 5.0}
            //};

            //var test = VectorUtilities.Transpose(m23);

            //var m2 = VectorUtilities.CreateMatrix(5, 5);

            //for (var i = 0; i < m1.Length; i++)
            //{
            //    for (var j = 0; j < m1.Length; j++)
            //    {
            //        m1[i][j] = i * j;
            //        m2[i][j] = i * j;
            //    }
            //}
            //for (var i = 0; i < m1.Length; i++)
            //{
            //    for (var j = 0; j < m1.Length; j++)
            //    {
            //        Console.Write($"{m1[i][j]} ");
            //    }
            //    Console.WriteLine();
            //}
            //for (var i = 0; i < m1.Length; i++)
            //{
            //    for (var j = 0; j < m1.Length; j++)
            //    {
            //        Console.Write($"{m2[i][j]} ");
            //    }
            //    Console.WriteLine();
            //}

            //var m3 = VectorUtilities.Multiply(m1, m2);
            //for (var i = 0; i < m1.Length; i++)
            //{
            //    for (var j = 0; j < m1.Length; j++)
            //    {
            //        Console.Write($"{m3[i][j]} ");
            //    }
            //    Console.WriteLine();
            //}

            var nnLayer1 = new NnLayer(100, 20);
            var nnLayer2 = new NnLayer(20, 5);

            nnLayer1.Next = nnLayer2;
            nnLayer2.Previous = nnLayer1;

            nnLayer1.ActivationFunction = new Relu();
            nnLayer2.ActivationFunction = new Softmax();

            var data = new double[20][];

            for (var i = 0; i < data.Length; i++)
            {
                data[i] = new double[100];
                for (var j = 0; j < 20; j++)
                {
                    data[i][j] = 0.01 * i * j;
                }
            }

            var data1 = new double[20][];

            for (var i = 0; i < data1.Length; i++)
            {
                data1[i] = new double[20];
                for (var j = 0; j < 20; j++)
                {
                    data1[i][j] = 0.01 * i * j;
                }
            }

            var opt = new GradientDescentOptimizer(new Softmax(), new CrossEntropy());

            var forwardPass = nnLayer1.FeedForward(data);

            var result = opt.CalculateGradient(data1[0], forwardPass[0], 3);

            nnLayer2.BackPropagate(result.WeightGradient, result.BiasGradient, 0.001);

            Console.Read();
        }

        private static double[][] MatMul(double[][] m1, double[][] m2)
        {
            var result = VectorUtilities.CreateMatrix(m1.Length, m2[0].Length);

            for (var i = 0; i < m1.Length; i++)
            {
                for (var j = 0; j < m2[0].Length; j++)
                {
                    for (var k = 0; k < m1[0].Length; k++)
                    {
                        result[i][j] += m1[i][k] * m2[k][j];
                    }
                }
            }

            return result;
        }
    }
}
