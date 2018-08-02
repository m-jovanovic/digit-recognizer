namespace DigitRecognizer.MachineLearning.Infrastructure.Functions
{
    /// <summary>
    /// Interface that all cost functions must implement.
    /// </summary>
    public interface ICostFunction : IDifferentiableFunction, IFunction
    {
        /// <summary>
        /// Gets the cost for the specified estimated and actual values.
        /// </summary>
        /// <param name="estimatedValues">The estimated values.</param>
        /// <param name="actualValues">The actual values.</param>
        /// <returns>The cost.</returns>
        double Cost(double[] estimatedValues, double[] actualValues);
    }
}
