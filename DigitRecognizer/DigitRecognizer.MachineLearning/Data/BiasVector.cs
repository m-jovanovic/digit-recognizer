using System;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Interfaces.Optimization;

namespace DigitRecognizer.MachineLearning.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class BiasVector : IValueAdjustable
    {
        private double[] _bias;

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
        }

        public void AdjustValue(double[][] gradient, double learningRate)
        {
            var rowCount = gradient.Length;
            var colCount = gradient[0].Length;

            var rowIndex = 1;
            Contracts.ValuesMatch(1, rowCount, nameof(rowCount));
            Contracts.ValuesMatch(_bias.Length, colCount, nameof(colCount));

            for (var i = 0; i < colCount; i++)
            {
                _bias[i] -= gradient[rowIndex][i] * learningRate;
            }
        }
    }
}
