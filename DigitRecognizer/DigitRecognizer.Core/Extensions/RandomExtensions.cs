using System;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Extensions
{
    /// <summary>
    /// Contains extension method for the <see cref="Random"/> class.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns an array of random floating-point nubmers that are greater than or equal to 0.0 and less than 1.0.
        /// </summary>
        /// <param name="rnd">The random number generator.</param>
        /// <param name="length">The length of the array.</param>
        /// <returns>The array filled with random numbers.</returns>
        public static double[] NextDoubles(this Random rnd, int length)
        {
            Contracts.ValueGreaterThanZero(length, nameof(length));

            var result = new double[length];

            for (var i = 0; i < length; i++)
            {
                result[i] = rnd.NextDouble();
            }

            return result;
        }
    }
}
