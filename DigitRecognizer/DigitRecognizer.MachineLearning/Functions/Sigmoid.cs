using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Interfaces.Functions;

namespace DigitRecognizer.MachineLearning.Functions
{
    public class Sigmoid : IActivationFunction
    {
        public string Name => "Sigmoid";

        public double[] Activate(double[] arr)
        {
            return MathUtilities.Sigmoid(arr);
        }

        public double Derivative(double[] input, int current, int oneHot)
        {
            var value = input[current];
            var sigmoid = MathUtilities.Sigmoid(value);
            var result = sigmoid / (1.0 - sigmoid);
            return result;
        }
    }
}
