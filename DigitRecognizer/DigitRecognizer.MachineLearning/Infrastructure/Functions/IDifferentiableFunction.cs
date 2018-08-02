namespace DigitRecognizer.MachineLearning.Infrastructure.Functions
{
    /// <summary>
    /// This interface should be implemented by all differentiable functions.
    /// </summary>
    public interface IDifferentiableFunction
    {
        /// <summary>
        /// Determines the derivative of the function for the specified inputs.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="oneHot">The one hot encoded element.</param>
        /// <returns>The derivative of the function for every input.</returns>
        double[] Derivative(double[] input, double[] oneHot);
    }
}
