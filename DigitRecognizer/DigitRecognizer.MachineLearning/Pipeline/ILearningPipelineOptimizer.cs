namespace DigitRecognizer.MachineLearning.Pipeline
{
    /// <summary>
    /// Interface for a <see cref="LearningPipeline"/> optimizer.
    /// </summary>
    public interface ILearningPipelineOptimizer : ILearningPipelineItem
    {
        /// <summary>
        /// Optimizes the specified parameters.
        /// </summary>
        /// <param name="predictions">The predictions.</param>
        /// <param name="oneHots">The one hot values.</param>
        void Optimize(double[][] predictions, int[] oneHots);
    }
}
