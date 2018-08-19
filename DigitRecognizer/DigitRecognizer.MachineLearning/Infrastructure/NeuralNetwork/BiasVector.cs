using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork
{
    /// <summary>
    /// Represents a bias vector that is part of a <see cref="NnLayer"/>.
    /// </summary>
    public class BiasVector : IValueAdjustable
    {
        private double[] _bias;

        /// <summary>
        /// Initializes a new instance of the <see cref="BiasVector"/> class with the specified value.
        /// </summary>
        /// <param name="bias"></param>
        public BiasVector(double[] bias)
        {
            _bias = bias;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BiasVector"/> class with the specified length.
        /// </summary>
        /// <param name="length">The length.</param>
        public BiasVector(int length)
        {
            Contracts.ValueGreaterThanZero(length, nameof(length));

            _bias = new double[length];
        }

        /// <summary>
        /// Gets the bias of the <see cref="BiasVector"/>.
        /// </summary>
        public double[] Bias => _bias;
        
        /// <summary>
        /// Gets the length of the <see cref="BiasVector"/>.
        /// </summary>
        public int Length => _bias.Length;

        /// <summary>
        /// Gets the size of the <see cref="BiasVector"/> in bytes.
        /// </summary>
        public int SizeInBytes => Length * sizeof(double);

        /// <summary>
        /// Adjusts the biases of the <see cref="BiasVector"/> using the specified gradient.
        /// </summary>
        /// <param name="gradient">The gradient with respect to the biases.</param>
        /// <param name="learningRate">The learning rate.</param>
        public void AdjustValue(double[][] gradient, double learningRate)
        {
            int rowCount = gradient.Length;
            int colCount = gradient[0].Length;

            const int rowIndex = 0;
            Contracts.ValuesMatch(1, rowCount, nameof(rowCount));
            Contracts.ValuesMatch(_bias.Length, colCount, nameof(colCount));

            for (var i = 0; i < colCount; i++)
            {
                _bias[i] = _bias[i] - gradient[rowIndex][i] * learningRate;
            }
        }

        public static implicit operator double[] (BiasVector b)
        {
            return b.Bias;
        }
    }
}
