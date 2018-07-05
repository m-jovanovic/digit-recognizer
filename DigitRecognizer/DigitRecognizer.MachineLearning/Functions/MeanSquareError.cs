using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Interfaces.Functions;

namespace DigitRecognizer.MachineLearning.Functions
{
    public class MeanSquareError : ILossFunction
    {
        public string Name => "Mean Square Error";

        public double Loss(double[] estimatedValues, double[] actualValues)
        {
            return MathUtilities.MeanSquareErr(estimatedValues, actualValues);
        }

        public double Derivative(double[] input, int current, int oneHot)
        {
            var delta = current == oneHot ? 1.0 : 0.0;
            var result = input[current] - delta;

            return result;
        }
    }
}
