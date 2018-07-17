using System;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Optimization;

namespace DigitRecognizer.MachineLearning.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class BiasVector : IValueAdjustable
    {
        private double[] _bias;

        public static implicit operator double[] (BiasVector b)
        {
            return b.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public double this[int i]
        {
            get
            {
                Contracts.ValueWithinBounds(i, 0, _bias.Length, nameof(i));

                return _bias[i];
            }
        }

        public double[] Value => _bias;

        public double[] Flattened => _bias;

        /// <summary>
        /// 
        /// </summary>
        public int Length => _bias.Length;

        /// <summary>
        /// 
        /// </summary>
        public int SizeInBytes => Length * sizeof(double);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bias"></param>
        public BiasVector(double[] bias)
        {
            _bias = bias;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        public BiasVector(int length)
        {
            Contracts.ValueGreaterThanZero(length, nameof(length));

            _bias = new double[length];
            var rnd = new Random();
            for (var i = 0; i < length; i++)
            {
                _bias[i] = rnd.NextDouble();
            }
        }

        public void AdjustValue(double[][] gradient, double learningRate)
        {
            var rowCount = gradient.Length;
            var colCount = gradient[0].Length;

            var rowIndex = 0;
            Contracts.ValuesMatch(1, rowCount, nameof(rowCount));
            Contracts.ValuesMatch(_bias.Length, colCount, nameof(colCount));

            for (var i = 0; i < colCount; i++)
            {
                _bias[i] -= gradient[rowIndex][i] * learningRate;
            }
        }
    }
}
