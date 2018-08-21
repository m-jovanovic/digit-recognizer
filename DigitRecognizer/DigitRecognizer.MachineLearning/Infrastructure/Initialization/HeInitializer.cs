using System;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Infrastructure.Initialization
{
    /// <summary>
    /// Implements the He initialization algorithm.
    /// </summary>
    public class HeInitializer : IInitializer
    {
        /// <summary>
        /// Gest the <see cref="Initialization.InitializerType"/>.
        /// </summary>
        public InitializerType InitializerType => InitializerType.HeInitialization;

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

            double factor = Math.Sqrt(2.0 / rowCount);

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
