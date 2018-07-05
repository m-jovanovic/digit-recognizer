using System.Dynamic;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Interfaces.Optimization;

namespace DigitRecognizer.MachineLearning.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class WeightMatrix : IValueAdjustable
    {
        private double[][] _weights;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public double[] this[int i]
        {
            get
            {
                Contracts.ValueWithinBounds(i, 0, _weights.Length, nameof(i));

                return _weights[i];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get
            {
                Contracts.ValueWithinBounds(i, 0, _weights.Length, nameof(i));
                Contracts.ValueWithinBounds(j, 0, _weights[0].Length, nameof(j));

                return _weights[i][j];
            }
        }
        
        public double[][] Value => _weights;
        
        /// <summary>
        /// 
        /// </summary>
        public int Height => _weights.Length;
        
        /// <summary>
        /// 
        /// </summary>
        public int Width => _weights[0].Length;

        /// <summary>
        /// 
        /// </summary>
        public double[] Flattened => Flatten();

        /// <summary>
        /// 
        /// </summary>
        public int SizeInBytes => Width * Height * sizeof(double);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weights"></param>
        public WeightMatrix(double[][] weights)
        {
            _weights = weights;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public WeightMatrix(int height, int width)
        {
            Contracts.ValueGreaterThanZero(height, nameof(height));
            Contracts.ValueGreaterThanZero(width, nameof(width));

            _weights = VectorUtilities.CreateMatrix(height, width);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private double[] Flatten()
        {
            return VectorUtilities.Flatten(_weights);
        }

        public void AdjustValue(double[][] gradient, double learningRate)
        {
            var rowCount = gradient.Length;
            var colCount = gradient[0].Length;
            Contracts.ValuesMatch(Height, rowCount, nameof(Height));
            Contracts.ValuesMatch(Width, colCount, nameof(Height));

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    _weights[i][j] -= gradient[i][j] * learningRate;
                }
            }
        }
    }
}
