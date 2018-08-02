using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Infrastructure.Functions
{
    /// <summary>
    /// Implements the sigmoid activation function.
    /// </summary>
    public class Sigmoid : IActivationFunction
    {
        public string Name => "Sigmoid";

        /// <summary>
        /// Applies the activation function on every element of the specified array.
        /// </summary>
        /// <param name="arr">The array of values.</param>
        /// <returns>The array with values fed through the activation function.</returns>
        public double[] Activate(double[] arr)
        {
            double[] activations = MathUtilities.Sigmoid(arr);

            return activations;
        }

        /// <summary>
        /// Determines the derivative of the <see cref="Sigmoid"/> function for the specified inputs.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="oneHot">The one hot encoded element.</param>
        /// <returns>The derivative of the function for every input.</returns>
        public double[] Derivative(double[] input, double[] oneHot)
        {
            var result = new double[input.Length];

            for (var i = 0; i < input.Length; i++)
            {
                double sigmoid = MathUtilities.Sigmoid(input[i]);

                result[i] = sigmoid / (1 - sigmoid);
            }

            return result;
        }
    }
}
