using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Functions
{
    public class LeakyRelu : IActivationFunction
    {
        private const double Alpha = 0.01;

        public string Name => "Leaky Relu";

        public double[] Activate(double[] arr)
        {
            return MathUtilities.LeakyRelu(arr, Alpha);
        }

        public double Derivative(double[] input, int current, int oneHot)
        {
            double result = input[current] >= 0 ? 1.0 : Alpha;

            return result;
        }
    }
}
