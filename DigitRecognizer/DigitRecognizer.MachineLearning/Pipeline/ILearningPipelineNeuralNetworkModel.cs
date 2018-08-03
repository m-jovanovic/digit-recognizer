namespace DigitRecognizer.MachineLearning.Pipeline
{
    /// <summary>
    /// Interface for a <see cref="LearningPipeline"/> neural network model.
    /// </summary>
    public interface ILearningPipelineNeuralNetworkModel : ILearningPipelineItem
    {
        /// <summary>
        /// Generates a prediction based on the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The prediction.</returns>
        double[][] Predict(double[][] input);
    }
}
