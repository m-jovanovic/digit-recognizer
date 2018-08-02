namespace DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork
{
    /// <summary>
    /// Interface that should be implemented by classes whose values should be adjusted.
    /// </summary>
    public interface IValueAdjustable
    {
        /// <summary>
        /// Adjusts the current values with specified gradient and learning rate.
        /// </summary>
        /// <param name="gradient">The gradient values.</param>
        /// <param name="learningRate">The learning rate.</param>
        void AdjustValue(double[][] gradient, double learningRate);
    }
}
