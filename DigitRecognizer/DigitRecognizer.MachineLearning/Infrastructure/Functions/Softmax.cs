using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Infrastructure.Functions
{
    /// <summary>
    /// Implements the softmax activation function.
    /// </summary>
    public class Softmax : IActivationFunction
    {
        public string Name => "Softmax";

        /// <summary>
        /// Applies the activation function on every element of the specified array.
        /// </summary>
        /// <param name="arr">The array of values.</param>
        /// <returns>The array with values fed through the activation function.</returns>
        public double[] Activate(double[] arr)
        {
            double[] activations = MathUtilities.Softmax(arr);

            return activations;
        }

        /// <summary>
        /// Determines the derivative of the <see cref="Softmax"/> function for the specified inputs.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="oneHot">The one hot encoded element.</param>
        /// <returns>The derivative of the function for every input.</returns>
        public double[] Derivative(double[] input, double[] oneHot)
        {
            var result = new double[input.Length];

            int oneHotIndex = oneHot.ArgMax();

            for (var i = 0; i < input.Length; i++)
            {
                double delta = i == oneHotIndex ? 1.0 : 0.0;

                result[i] = input[i] * (delta - input[oneHotIndex]);
            }

            return result;
        }
    }
}
