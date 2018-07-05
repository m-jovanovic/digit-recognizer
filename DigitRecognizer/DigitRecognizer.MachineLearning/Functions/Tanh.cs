using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Interfaces.Functions;

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
            var value = input[current];
            var tanh = MathUtilities.Tanh(value);
            var result = 1 - tanh * tanh;

            return result;
        }
    }
}
