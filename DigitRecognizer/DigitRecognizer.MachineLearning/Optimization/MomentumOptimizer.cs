using System.Collections.Generic;
using System.Linq;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Functions;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;
using DigitRecognizer.MachineLearning.Pipeline;

namespace DigitRecognizer.MachineLearning.Optimization
{
    /// <summary>
    /// Implements the gradient descent with momentum optimization algorithm.
    /// </summary>
    public class MomentumOptimizer : IOptimizer
    {
        private readonly ICostFunction _costFunction;
        private readonly INeuralNetwork _neuralNetwork;
        private List<double[][]> _weightedSumDerivatives;
        private List<double[][]> _activations;

        /// <summary>
        /// initilazies a new instance of the <see cref="MomentumOptimizer"/> class.
        /// </summary>
        /// <param name="neuralNetwork">The neural network.</param>
        /// <param name="costFunction">The cost function.</param>
        public MomentumOptimizer(INeuralNetwork neuralNetwork, ICostFunction costFunction)
        {
            Contracts.ValueNotNull(costFunction, nameof(neuralNetwork));
            Contracts.ValueNotNull(costFunction, nameof(costFunction));

            _neuralNetwork = neuralNetwork;
            _costFunction = costFunction;
        }

        /// <summary>
        /// Gets the <see cref="INeuralNetwork"/>.
        /// </summary>
        public INeuralNetwork NeuralNetwork => _neuralNetwork;

        /// <summary>
        /// Gets the <see cref="ICostFunction"/>.
        /// </summary>
        public ICostFunction CostFunction => _costFunction;

        /// <summary>
        /// Calculates the cost of the specified prediction.
        /// </summary>
        /// <param name="prediction">The prediction.</param>
        /// <param name="oneHot">The one hot value.</param>
        /// <returns>The cost.</returns>
        public double CalculateError(double[] prediction, int oneHot)
        {
            double error = _costFunction.Cost(prediction, oneHot.OneHot(prediction.Length));

            return error;
        }

        /// <summary>
        /// Optimizes the specified parameters.
        /// </summary>
        /// <param name="predictions">The predictions.</param>
        /// <param name="oneHots">The one hot values.</param>
        public void Optimize(double[][] predictions, int[] oneHots)
        {
            if (PipelineSettings.Instance.CurrentIteration % 10 == 0)
            {
                double error = predictions.Select((prediction, i) => CalculateError(prediction, oneHots[i])).Sum();

                error /= predictions.Length;

                ConsoleUtility.WriteLine($"Eror for iteration {PipelineSettings.Instance.CurrentIteration}: {error}");
            }

            Backpropagate(predictions, oneHots);
        }

        /// <summary>
        /// Peforms the backpropagatin algorithm.
        /// </summary>
        /// <param name="predictions">The predictions.</param>
        /// <param name="oneHots">The one hot values.</param>
        public void Backpropagate(double[][] predictions, int[] oneHots)
        {
            ProcessCaches(oneHots);

            double[][] gradients = CalculateOutputDerivative(predictions, oneHots);

            for (var i = 0; i < predictions.Length; i++)
            {
                double[][] currentGradient = gradients[i].AsMatrix();

                Core.Data.LinkedListNode<NnLayer> currentLayer = _neuralNetwork.Layers.Last;

                while (currentLayer != null)
                {
                    double[][] delta = currentGradient;

                    // We don't multiply with the weighed sum derivative for the last layer in the network.
                    if (currentLayer.Depth < _neuralNetwork.Layers.Count - 1)
                    {
                        delta = delta.HadamardProduct(_weightedSumDerivatives[currentLayer.Depth][i].AsMatrix());
                    }

                    double[][] activation = _activations[currentLayer.Depth][i].AsMatrix();

                    double[][] weightGradients = activation.Transpose().Multiply(delta);

                    AdjustParameters(currentLayer.Value, delta, weightGradients, _neuralNetwork.LearningRate);

                    currentGradient = currentLayer.Value.BackpropagateError(delta);

                    currentLayer = currentLayer.Previous;
                }
            }
        }

        /// <summary>
        /// Adjusts the current parameters with specified gradient and learning rate.
        /// </summary>
        /// <param name="layer">The layer.</param>
        /// <param name="delta">The delat values.</param>
        /// <param name="gradient">The gradient values.</param>
        /// <param name="learningRate">The learning rate.</param>
        public void AdjustParameters(NnLayer layer, double[][] delta, double[][] gradient, double learningRate)
        {
            int rowCount = delta.Length;
            int colCount = delta[0].Length;

            const int rowIndex = 0;
            Contracts.ValuesMatch(1, rowCount, nameof(rowCount));
            Contracts.ValuesMatch(layer.BiasVector.Length, colCount, nameof(colCount));

            for (var i = 0; i < colCount; i++)
            {
                layer.BiasVector.Bias[i] = layer.BiasVector.Bias[i] - delta[rowIndex][i] * learningRate;
            }

            rowCount = gradient.Length;
            colCount = gradient[0].Length;
            Contracts.ValuesMatch(layer.WeightMatrix.RowCount, rowCount, nameof(layer.WeightMatrix.RowCount));
            Contracts.ValuesMatch(layer.WeightMatrix.ColCount, colCount, nameof(layer.WeightMatrix.RowCount));

            var regularizationFactor = 1.0;

            if (PipelineSettings.Instance.UseL2Regularization)
            {
                regularizationFactor = 1 - PipelineSettings.Instance.RegularizationFactor * learningRate / PipelineSettings.Instance.DatasetSize;
            }

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    layer.WeightMatrix.Weights[i][j] = regularizationFactor * layer.WeightMatrix.Weights[i][j] - gradient[i][j] * learningRate;
                }
            }
        }

        /// <summary>
        /// Calculates the weighted sum derivatives and activation caches in advance.
        /// </summary>
        /// <param name="oneHots">The one hot values.</param>
        private void ProcessCaches(int[] oneHots)
        {
            var wSumDerivs = new List<double[][]>();
            var activationCaches = new List<double[][]>();

            Core.Data.LinkedListNode<NnLayer> currentLayer = _neuralNetwork.Layers.First;

            while (currentLayer != null)
            {
                double[][] wSumDeriv = WeightedSumDerivative(currentLayer.Value.ActivationFunction, currentLayer.Depth, oneHots);

                wSumDerivs.Add(wSumDeriv);

                double[][] activationCache = _neuralNetwork.ActivationCache[currentLayer.Depth];

                activationCaches.Add(activationCache);

                currentLayer = currentLayer.Next;
            }

            _weightedSumDerivatives = wSumDerivs;
            _activations = activationCaches;
        }

        /// <summary>
        /// Calculates the output derivative with respect to the <see cref="ICostFunction"/> of the optimizer.
        /// </summary>
        /// <param name="predictions">The predictions.</param>
        /// <param name="oneHots">The one hot values.</param>
        /// <returns>The derivatives.</returns>
        public double[][] CalculateOutputDerivative(double[][] predictions, int[] oneHots)
        {
            int count = predictions.Length;
            int length = predictions[0].Length;
            double[][] gradient = VectorUtilities.CreateMatrix(count, length);

            for (var i = 0; i < count; i++)
            {
                gradient[i] = CostFunction.Derivative(predictions[i], oneHots[i].OneHot(length));

                if (PipelineSettings.Instance.UseGradientClipping)
                {
                    const double threshold = 2.5;

                    double l2Norm = MathUtilities.L2Norm(gradient[i]);

                    if (l2Norm > threshold)
                    {
                        for (var j = 0; j < length; j++)
                        {
                            gradient[i][j] = gradient[i][j] * threshold / l2Norm;
                        }
                    }
                }
            }

            return gradient;
        }

        /// <summary>
        /// Calcualates the derivative of the weighted sum with respect to the specified <see cref="IActivationFunction"/>.
        /// </summary>
        /// <param name="activationFunction">The activation function.</param>
        /// <param name="nodeDepth">The node depth.</param>
        /// <param name="oneHots">The one hot values.</param>
        /// <returns>The derivatives.</returns>
        public double[][] WeightedSumDerivative(IActivationFunction activationFunction, int nodeDepth, int[] oneHots)
        {
            double[][] cachedWeightedSum = _neuralNetwork.WeightedSumCache[nodeDepth];

            int rowCount = cachedWeightedSum.Length;
            int colCount = cachedWeightedSum[0].Length;
            double[][] result = VectorUtilities.CreateMatrix(rowCount, colCount);

            for (var i = 0; i < rowCount; i++)
            {
                result[i] = activationFunction.Derivative(cachedWeightedSum[i], oneHots[i].OneHot(colCount));
            }

            return result;
        }
    }
}
