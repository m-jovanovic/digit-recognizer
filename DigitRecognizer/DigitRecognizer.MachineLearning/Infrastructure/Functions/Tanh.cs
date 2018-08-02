using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Infrastructure.Functions
{
    /// <summary>
    /// Implements the hyperbolic tangent activation function.
    /// </summary>
    public class Tanh : IActivationFunction
    {
        public string Name => "Tanh";

        /// <summary>
        /// Applies the activation function on every element of the specified array.
        /// </summary>
        /// <param name="arr">The array of values.</param>
        /// <returns>The array with values fed through the activation function.</returns>
        public double[] Activate(double[] arr)
        {
            double[] activations = MathUtilities.Tanh(arr);

            return activations;
        }

        /// <summary>
        /// Determines the derivative of the <see cref="Tanh"/> function for the specified inputs.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="oneHot">The one hot encoded element.</param>
        /// <returns>The derivative of the function for every input.</returns>
        public double[] Derivative(double[] input, double[] oneHot)
        {
            var result = new double[input.Length];

            for (var i = 0; i < input.Length; i++)
            {
                double tanh = MathUtilities.Tanh(input[i]);

                result[i] = 1 - tanh * tanh;
            }

            return result;
        }
    }
}
