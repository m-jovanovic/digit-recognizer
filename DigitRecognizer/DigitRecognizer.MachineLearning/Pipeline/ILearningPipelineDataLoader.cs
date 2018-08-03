namespace DigitRecognizer.MachineLearning.Pipeline
{
    /// <summary>
    /// Interface for a <see cref="LearningPipeline"/> data loader.
    /// </summary>
    public interface ILearningPipelineDataLoader : ILearningPipelineItem
    {
        /// <summary>
        /// Loads the data from a source file.
        /// </summary>
        /// <returns>The data object.</returns>
        object LoadData();
    }
}
