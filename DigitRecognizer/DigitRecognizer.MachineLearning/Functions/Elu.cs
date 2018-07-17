using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Functions
{
    public class Elu : IActivationFunction
    {
        private const double Alpha = 0.01;

        public string Name => "Exponential Relu";

        public double[] Activate(double[] arr)
        {
            return MathUtilities.Elu(arr, Alpha);
        }

        public double Derivative(double[] input, int current, int oneHot)
        {
            double value = input[current];
            double result = value >= 0 ? 1.0 : MathUtilities.Elu(value, Alpha) + Alpha;

            return result;
        }
    }
}
