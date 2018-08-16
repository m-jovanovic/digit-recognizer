namespace DigitRecognizer.MachineLearning.Optimization.LearningRateDecay
{
    /// <summary>
    /// Interface for a learning rate decay method.
    /// </summary>
    public interface ILearningRateDecay
    {
        /// <summary>
        /// Calculates the learning rate.
        /// </summary>
        /// <param name="learningRate">The current learning rate.</param>
        /// <returns>The new learning rate.</returns>
        double DecayLearningRate(double learningRate);
    }
}
