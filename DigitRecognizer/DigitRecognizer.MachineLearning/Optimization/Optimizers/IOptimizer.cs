using DigitRecognizer.Core.Data;
using DigitRecognizer.MachineLearning.Infrastructure.Functions;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;
using DigitRecognizer.MachineLearning.Pipeline;

namespace DigitRecognizer.MachineLearning.Optimization.Optimizers
{
    /// <summary>
    /// Interface that an optimization algorithm should implement.
    /// </summary>
    public interface IOptimizer : ILearningPipelineOptimizer
    {
        /// <summary>
        /// Gets the <see cref="INeuralNetwork"/>.
        /// </summary>
        INeuralNetwork NeuralNetwork { get; }

        /// <summary>
        /// Gets the <see cref="ICostFunction"/>.
        /// </summary>
        ICostFunction CostFunction { get; }

        /// <summary>
        /// Calculates the cost of the specified prediction.
        /// </summary>
        /// <param name="prediction">The prediction.</param>
        /// <param name="oneHot">The one hot value.</param>
        /// <returns>The cost.</returns>
        double CalculateError(double[] prediction, int oneHot);

        /// <summary>
        /// Peforms the backpropagatin algorithm.
        /// </summary>
        /// <param name="predictions">The predictions.</param>
        /// <param name="oneHots">The one hot values.</param>
        void Backpropagate(double[][] predictions, int[] oneHots);

        /// <summary>
        /// Adjusts the current parameters with specified gradient and learning rate.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="delta">The delat values.</param>
        /// <param name="gradient">The gradient values.</param>
        /// <param name="learningRate">The learning rate.</param>
        void AdjustParameters(LinkedListNode<NnLayer> node, double[][] delta, double[][] gradient, double learningRate);

        /// <summary>
        /// Calculates the output derivative with respect to the <see cref="ICostFunction"/> of the optimizer.
        /// </summary>
        /// <param name="predictions">The predictions.</param>
        /// <param name="oneHots">The one hot values.</param>
        /// <returns>The derivatives.</returns>
        double[][] CalculateOutputDerivative(double[][] predictions, int[] oneHots);

        /// <summary>
        /// Calcualates the derivative of the weighted sum with respect to the specified <see cref="IActivationFunction"/>.
        /// </summary>
        /// <param name="activationFunction">The activation function.</param>
        /// <param name="nodeDepth">The node depth.</param>
        /// <param name="oneHots">The one hot values.</param>
        /// <returns>The derivatives.</returns>
        double[][] WeightedSumDerivative(IActivationFunction activationFunction, int nodeDepth, int[] oneHots);
    }
}
