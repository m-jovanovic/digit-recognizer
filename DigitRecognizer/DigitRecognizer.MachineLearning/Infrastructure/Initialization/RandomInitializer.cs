using System;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Infrastructure.Initialization
{
    /// <summary>
    /// Implements the random initialization algorithm.
    /// </summary>
    public class RandomInitializer : IInitializer
    {
        /// <summary>
        /// Gest the <see cref="Initialization.InitializerType"/>.
        /// </summary>
        public InitializerType InitializerType => InitializerType.RandomInitialization;

        /// <summary>
        /// Returns an initialized matrix based on the specified parameters.
        /// </summary>
        /// <param name="rowCount">The row count.</param>
        /// <param name="colCont">The col count.</param>
        /// <returns>The initialized matrix.</returns>
        public double[][] Initialize(int rowCount, int colCont)
        {
            double[][] result = VectorUtilities.CreateMatrix(rowCount, colCont);

            var random = new Random();

            const double factor = 0.01;

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCont; j++)
                {
                    result[i][j] = random.NextDouble() * factor;
                }
            }

            return result;
        }
    }
}
