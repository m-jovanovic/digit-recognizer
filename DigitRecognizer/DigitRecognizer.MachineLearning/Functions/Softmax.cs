using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Functions
{
    public class Softmax : IActivationFunction
    {
        public string Name => "Softmax";

        public double[] Activate(double[] arr)
        {
            return MathUtilities.Softmax(arr);
        }

        public double Derivative(double[] input, int current, int oneHot)
        {
            double delta = current == oneHot ? 1.0 : 0.0;

            double result = input[current] * (delta - input[oneHot]);

            return result;
        }
    }
}
