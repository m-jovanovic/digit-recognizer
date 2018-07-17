using DigitRecognizer.Core.Utilities;

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
            double value = input[current];
            double sigmoid = MathUtilities.Sigmoid(value);
            double result = sigmoid / (1.0 - sigmoid);
            return result;
        }
    }
}
