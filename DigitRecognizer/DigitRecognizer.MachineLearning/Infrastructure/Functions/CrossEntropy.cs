using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Infrastructure.Functions
{
    /// <summary>
    /// Implements the cross entropy cost function.
    /// </summary>
    public class CrossEntropy : ICostFunction
    {
        public string Name => "Cross Entropy";

        /// <summary>
        /// Calculates the cost for the specified estimated and actual values.
        /// </summary>
        /// <param name="estimatedValues">The estimated values.</param>
        /// <param name="actualValues">The actual values.</param>
        /// <returns>The cost.</returns>
        public double Cost(double[] estimatedValues, double[] actualValues)
        {
            double cost = MathUtilities.CrossEntropy(estimatedValues, actualValues);

            return cost;
        }

        /// <summary>
        /// Gets the derivative of the <see cref="CrossEntropy"/> function for the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="oneHot">The one hot encoded array.</param>
        /// <returns>The derivative with respect to each input.</returns>
        public double[] Derivative(double[] input, double[] oneHot)
        {
            var result = new double[input.Length];

            int oneHotIndex = oneHot.ArgMax();

            for (var i = 0; i < input.Length; i++)
            {
                double delta = i == oneHotIndex ? 1.0 : 0.0;

                result[i] = input[i] - delta;
            }

            return result;
        }
    }
}
