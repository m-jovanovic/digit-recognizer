namespace DigitRecognizer.MachineLearning.Infrastructure.Functions
{
    /// <summary>
    /// Implement that all activation functions must implement.
    /// </summary>
    public interface IActivationFunction : IDifferentiableFunction, IFunction
    {
        /// <summary>
        /// Applies the activation function on every element of the specified array.
        /// </summary>
        /// <param name="arr">The array of values.</param>
        /// <returns>The array with values fed through the activation function.</returns>
        double[] Activate(double[] arr);
    }
}
