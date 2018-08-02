using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Infrastructure.Initialization
{
    /// <summary>
    /// Implements the zero initialization algorithm.
    /// </summary>
    public class ZeroInitializer : IInitializer
    {
        /// <summary>
        /// Gest the <see cref="Initialization.InitializerType"/>.
        /// </summary>
        public InitializerType InitializerType => InitializerType.ZeroInitialization;

        /// <summary>
        /// Returns an initialized matrix based on the specified parameters.
        /// </summary>
        /// <param name="rowCount">The row count.</param>
        /// <param name="colCont">The col count.</param>
        /// <returns>The initialized matrix.</returns>
        public double[][] Initialize(int rowCount, int colCont)
        {
            return VectorUtilities.CreateMatrix(rowCount, colCont);
        }
    }
}
