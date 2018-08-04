using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Factories;
using DigitRecognizer.MachineLearning.Infrastructure.Initialization;
using DigitRecognizer.MachineLearning.Pipeline;

namespace DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork
{
    /// <summary>
    /// Represents a weight matrix that is part of a <see cref="NnLayer"/>.
    /// </summary>
    public class WeightMatrix : IValueAdjustable
    {
        private readonly double[][] _weights;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightMatrix"/> class with the specified parameters.
        /// </summary>
        /// <param name="rowCount">The row count.</param>
        /// <param name="colCount">The col count.</param>
        public WeightMatrix(int rowCount, int colCount)
        {
            Contracts.ValueGreaterThanZero(rowCount, nameof(rowCount));
            Contracts.ValueGreaterThanZero(colCount, nameof(colCount));

            IInitializer initializer = InitializerFactory.Instance.GetInstance(PipelineSettings.Instance.WeightsInitializerType);

            _weights = initializer.Initialize(rowCount, colCount);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightMatrix"/> class with the specified value.
        /// </summary>
        /// <param name="weights">The weights.</param>
        public WeightMatrix(double[][] weights)
        {
            _weights = weights;
        }

        /// <summary>
        /// Gets the <see cref="WeightMatrix"/> row count.
        /// </summary>
        public int RowCount => _weights.Length;

        /// <summary>
        /// Gets the <see cref="WeightMatrix"/> column count.
        /// </summary>
        public int ColCount => _weights[0].Length;

        /// <summary>
        /// Gets the flattened <see cref="WeightMatrix"/>.
        /// </summary>
        public double[] Flattened => VectorUtilities.Flatten(_weights);

        /// <summary>
        /// Gets the size of the <see cref="WeightMatrix"/> in the bytes.
        /// </summary>
        public int SizeInBytes => ColCount * RowCount * sizeof(double);

        /// <summary>
        /// Gets the weights of the <see cref="WeightMatrix"/>.
        /// </summary>
        public double[][] Weights => _weights;

        /// <summary>
        /// Adjusts the weights of the <see cref="WeightMatrix"/> using the specified gradient.
        /// </summary>
        /// <param name="gradient">The gradient with respect to the weights.</param>
        /// <param name="learningRate">The learning rate.</param>
        public void AdjustValue(double[][] gradient, double learningRate)
        {
            int rowCount = gradient.Length;
            int colCount = gradient[0].Length;
            Contracts.ValuesMatch(RowCount, rowCount, nameof(RowCount));
            Contracts.ValuesMatch(ColCount, colCount, nameof(RowCount));

            var regularizationFactor = 1.0;

            if (PipelineSettings.Instance.UseL2Regularization)
            {
                regularizationFactor = 1 - PipelineSettings.Instance.RegularizationFactor * learningRate / PipelineSettings.Instance.DatasetSize;
            }

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    _weights[i][j] = regularizationFactor * _weights[i][j] - gradient[i][j] * learningRate;
                }
            }
        }

        public static implicit operator double[][] (WeightMatrix m)
        {
            return m._weights;
        }
    }
}
