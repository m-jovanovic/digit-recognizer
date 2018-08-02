namespace DigitRecognizer.MachineLearning.Infrastructure.Initialization
{
    /// <summary>
    /// Interface for an initialization algorithm.
    /// </summary>
    public interface IInitializer
    {
        /// <summary>
        /// Gets the <see cref="Initialization.InitializerType"/>.
        /// </summary>
        InitializerType InitializerType { get; }

        /// <summary>
        /// Returns an initialized matrix based on the specified parameters.
        /// </summary>
        /// <param name="rowCount">The row count.</param>
        /// <param name="colCont">The col count.</param>
        /// <returns>The initialized matrix.</returns>
        double[][] Initialize(int rowCount, int colCont);
    }
}
