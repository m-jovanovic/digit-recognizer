using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Functions
{
    public class Tanh : IActivationFunction
    {
        public string Name => "Tanh";

        public double[] Activate(double[] arr)
        {
            return MathUtilities.Tanh(arr);
        }

        public double Derivative(double[] input, int current, int oneHot)
        {
            double value = input[current];
            double tanh = MathUtilities.Tanh(value);
            double result = 1 - tanh * tanh;

            return result;
        }
    }
}
