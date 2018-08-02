namespace DigitRecognizer.MachineLearning.Infrastructure.Functions
{
    /// <summary>
    /// Interface that all function must implement.
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        string Name { get; }
    }
}
