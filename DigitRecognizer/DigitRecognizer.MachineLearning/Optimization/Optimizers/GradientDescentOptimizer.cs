using DigitRecognizer.MachineLearning.Infrastructure.Functions;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;

namespace DigitRecognizer.MachineLearning.Optimization.Optimizers
{
    /// <summary>
    /// Implements the gradient descent optimization algorithm.
    /// </summary>
    public class GradientDescentOptimizer : BaseOptimizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GradientDescentOptimizer"/> class.
        /// </summary>
        /// <param name="neuralNetwork">The neural network.</param>
        /// <param name="costFunction">The cost function.</param>
        public GradientDescentOptimizer(INeuralNetwork neuralNetwork, ICostFunction costFunction) 
            : base(neuralNetwork, costFunction)
        {
        }
    }
}
