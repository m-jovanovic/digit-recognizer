using DigitRecognizer.MachineLearning.Pipeline;

namespace DigitRecognizer.MachineLearning.Providers
{
    /// <summary>
    /// Represents a data provider abstraction that a neural network will use for training.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProvider<out T> : ILearningPipelineDataLoader
    {
        /// <summary>
        /// Gets the data from a file.
        /// </summary>
        /// <returns></returns>
        T GetData();
    }
}
