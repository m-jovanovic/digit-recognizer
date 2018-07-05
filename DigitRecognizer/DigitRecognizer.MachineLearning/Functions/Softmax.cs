using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Interfaces.Functions;

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
            var delta = current == oneHot ? 1.0 : 0.0;

            var result = input[current] * (delta - input[oneHot]);

            return result;
        }
    }
}
