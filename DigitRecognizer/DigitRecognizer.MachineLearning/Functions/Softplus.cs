using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Functions
{
    public class Softplus : IActivationFunction
    {
        public string Name => "Softplus";

        public double[] Activate(double[] arr)
        {
            return MathUtilities.Softplus(arr);
        }

        public double Derivative(double[] input, int current, int oneHot)
        {
            double result = MathUtilities.Sigmoid(input[current]);

            return result;
        }
    }
}
