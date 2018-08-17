using System;
using DigitRecognizer.Core.Extensions;

namespace DigitRecognizer.Core.Utilities
{
    /// <summary>
    /// Contains utility methods for commonly required math functions.
    /// </summary>
    public static class MathUtilities
    {
        /// <summary>
        /// Computes the softmax function for the specified array.
        /// </summary>
        /// <param name="arr">The array with values for the computation.</param>
        /// <returns>The array with softmax values.</returns>
        public static double[] Softmax(double[] arr)
        {
            int length = arr.Length;
            var result = new double[length];

            // The epsilon is used for numerical stability.
            double epsilon = -arr.Max();

            for (var i = 0; i < length; i++)
            {
                result[i] = Softmax(arr[i] + epsilon);
            }

            double denominator = result.Sum();
            for (var i = 0; i < length; i++)
            {
                result[i] /= denominator;
            }

            return result;
        }

        /// <summary>
        /// Computes the softmax function for the specified value.
        /// </summary>
        /// <param name="val">The value that is used for the computation.</param>
        /// <returns>The result of the softmax function.</returns>
        public static double Softmax(double val)
        {
            return Math.Exp(val);
        }

        /// <summary>
        /// Computes the sigmoid function for the specified array.
        /// </summary>
        /// <param name="arr">The array with values for the computation.</param>
        /// <returns>The array with sigmoid values.</returns>
        public static double[] Sigmoid(double[] arr)
        {
            int length = arr.Length;
            var result = new double[length];

            // The epsilon is used for numerical stability.
            double epsilon = arr.Max();

            for (var i = 0; i < length; i++)
            {
                result[i] = Sigmoid(-arr[i] + epsilon);
            }

            return result;
        }

        /// <summary>
        /// Computes the sigmoid function for the specified value.
        /// </summary>
        /// <param name="val">The value that is used for the computation.</param>
        /// <returns>The result of the sigmodi function.</returns>
        public static double Sigmoid(double val)
        {
            return 1.0 / (1 + Math.Exp(val));
        }

        /// <summary>
        /// Computes the RELU function for the specified array.
        /// </summary>
        /// <param name="arr">The array with values for the computation.</param>
        /// <returns>The array with RELU values.</returns>
        public static double[] Relu(double[] arr)
        {
            int length = arr.Length;
            var result = new double[length];

            for (var i = 0; i < length; i++)
            {
                result[i] = Relu(arr[i]);
            }

            return result;
        }

        /// <summary>
        /// Computes the RELU function for the specified value.
        /// </summary>
        /// <param name="val">The value that is used for the computation.</param>
        /// <returns>The result of the RELU function.</returns>
        public static double Relu(double val)
        {
            return val > 0.0 ? val : 0.0;
        }

        /// <summary>
        /// Computes the softplus function for the specified array.
        /// </summary>
        /// <param name="arr">The array with values for the computation.</param>
        /// <returns>The array with softplus values.</returns>
        public static double[] Softplus(double[] arr)
        {
            int length = arr.Length;
            var result = new double[length];

            // The epsilon is used for numerical stability.
            double epsilon = -arr.Max();

            for (var i = 0; i < length; i++)
            {
                result[i] = Softplus(arr[i] + epsilon);
            }

            return result;
        }

        /// <summary>
        /// Computes the softplus function for the specified value.
        /// </summary>
        /// <param name="val">The value that is used for the computation.</param>
        /// <returns>The result of the soft plus function.</returns>
        public static double Softplus(double val)
        {
            return Math.Log(1 + Math.Exp(val));
        }

        /// <summary>
        /// Computes the tanh function for the specified array.
        /// </summary>
        /// <param name="arr">The array with values for the computation.</param>
        /// <returns>The array with tanh values.</returns>
        public static double[] Tanh(double[] arr)
        {
            int length = arr.Length;
            var result = new double[length];

            // The epsilon is used for numerical stability.
            double epsilon = arr.Max();

            for (var i = 0; i < length; i++)
            {
                result[i] = Tanh(arr[i] - epsilon);
            }

            return result;
        }

        /// <summary>
        /// Computes the tanh function for the specified value.
        /// </summary>
        /// <param name="val">The value that is used for the computation.</param>
        /// <returns>The result of the tanh fucntion.</returns>
        public static double Tanh(double val)
        {
            return 2.0 / (1.0 + Math.Exp(-2 * val)) - 1;
        }

        /// <summary>
        /// Computes the param. RELU function for the specified array.
        /// </summary>
        /// <param name="arr">The array with values for the computation.</param>
        /// <param name="alpha"></param>
        /// <returns>The param. RELU value array.</returns>
        public static double[] LeakyRelu(double[] arr, double alpha)
        {
            int length = arr.Length;
            var result = new double[length];

            for (var i = 0; i < length; i++)
            {
                result[i] = LeakyRelu(arr[i], alpha);
            }

            return result;
        }

        /// <summary>
        /// Computes the param. RELU function for the specified value.
        /// </summary>
        /// <param name="val">The value that is used for the computation.</param>
        /// <param name="alpha"></param>
        /// <returns>The param. RELU value.</returns>
        public static double LeakyRelu(double val, double alpha)
        {
            return val < 0 ? alpha * val : val;
        }

        /// <summary>
        /// Computes the exp. RELU function for the specified array.
        /// </summary>
        /// <param name="arr">The array with values for the computation.</param>
        /// <param name="alpha"></param>
        /// <returns>The exp. RELU value array.</returns>
        public static double[] ExponentialRelu(double[] arr, double alpha)
        {
            int length = arr.Length;
            var result = new double[length];

            // The epsilon is used for numerical stability.
            double epsilon = -arr.Max();

            for (var i = 0; i < arr.Length; i++)
            {
                result[i] = ExponentialRelu(arr[i] < 0 ? arr[i] + epsilon : arr[i], alpha);
            }

            return result;
        }

        /// <summary>
        /// Computes the ELU function for the specified value.
        /// </summary>
        /// <param name="val">The value that is used for the computation.</param>
        /// <param name="alpha"></param>
        /// <returns>The exp. RELU value.</returns>
        public static double ExponentialRelu(double val, double alpha)
        {
            return val < 0 ? alpha * (Math.Exp(val) - 1) : val;
        }

        /// <summary>
        /// Computes the cross entropy function for the specified array.
        /// </summary>
        /// <param name="estArr">The array with estimated values.</param>
        /// <param name="actArr">The array with actual values.</param>
        /// <returns>The cross entropy loss between two vectors.</returns>
        public static double CrossEntropy(double[] estArr, double[] actArr)
        {
            Contracts.ValuesMatch(estArr.Length, actArr.Length, nameof(estArr.Length));

            int actValIndex = actArr.ArgMax();

            double result = - Math.Log(estArr[actValIndex] + double.Epsilon);

            return result;
        }

        /// <summary>
        /// Computes the MSE function for the specified arrays.
        /// </summary>
        /// <param name="estArr">The array with estimated values.</param>
        /// <param name="actArr">The array with actual values.</param>
        /// <returns>The MSE err of the two vectors.</returns>
        public static double MeanSquareErr(double[] estArr, double[] actArr)
        {
            Contracts.ValuesMatch(estArr.Length, actArr.Length, nameof(estArr.Length));

            int length = estArr.Length;
            var result = 0.0;

            for (var i = 0; i < length; i++)
            {
                result += SquareDistance(estArr[i], actArr[i]);
            }

            result /= 2 * length;

            return result;
        }

        /// <summary>
        /// Computes the Euclidean distance function for the specified arrays.
        /// </summary>
        /// <param name="estArr">The array with estimated values.</param>
        /// <param name="actArr">The array with actual values.</param>
        /// <returns>The euclidean distance between two vectors.</returns>
        public static double EuclideanDistance(double[] estArr, double[] actArr)
        {
            Contracts.ValuesMatch(estArr.Length, actArr.Length, nameof(estArr.Length));

            int length = estArr.Length;
            var result = 0.0;

            for (var i = 0; i < length; i++)
            {
                result += SquareDistance(estArr[i], actArr[i]);
            }

            result = Math.Sqrt(result);

            return result;
        }

        /// <summary>
        /// Computes the square distance function for the specified values.
        /// </summary>
        /// <param name="estVal">The estimated value.</param>
        /// <param name="actVal">The actual value.</param>
        /// <returns>The square distance between the values.</returns>
        public static double SquareDistance(double estVal, double actVal)
        {
            double result = estVal - actVal;

            return result * result;
        }

        /// <summary>
        /// Calculates the L1 norm of the specified input.
        /// </summary>
        /// <param name="arr">The array with values for the computation.</param>
        /// <returns>The norm.</returns>
        public static double L1Norm(double[] arr)
        {
            var result = 0.0;
            int length = arr.Length;
            for (var i = 0; i < length; i++)
            {
                result += arr[i];
            }

            result = ZeroSumCheck(result);

            return result;
        }

        /// <summary>
        /// Calculates the L2 norm of the specified input.
        /// </summary>
        /// <param name="arr">The array with values for the computation.</param>
        /// <returns>The norm.</returns>
        public static double L2Norm(double[] arr)
        {
            var result = 0.0;
            int length = arr.Length;
            for (var i = 0; i < length; i++)
            {
                result += Math.Pow(arr[i], 2);
            }

            result = Math.Sqrt(result);

            result = ZeroSumCheck(result);

            return result;
        }

        /// <summary>
        /// Checks if the specified value is possibly a 0.
        /// </summary>
        /// <param name="value">The value that is checked.</param>
        /// <returns>A very small positive value - epsilon, or the value passed in.</returns>
        private static double ZeroSumCheck(double value)
        {
            return Math.Abs(value) < double.Epsilon && Math.Abs(value) > -double.Epsilon ? double.Epsilon : value;
        }
    }
}
