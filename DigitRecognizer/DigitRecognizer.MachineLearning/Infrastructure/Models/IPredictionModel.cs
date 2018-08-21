namespace DigitRecognizer.MachineLearning.Infrastructure.Models
{
    /// <summary>
    /// Interface for a prediction model.
    /// </summary>
    public interface IPredictionModel
    {
        /// <summary>
        /// Predicts the output for the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The output as a vector of probabilites.</returns>
        double[] Predict(double[] input);
    }
}
