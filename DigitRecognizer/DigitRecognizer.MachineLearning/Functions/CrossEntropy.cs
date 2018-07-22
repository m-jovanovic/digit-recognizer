﻿using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Functions
{
    public class CrossEntropy : ILossFunction
    {
        public string Name => "Cross Entropy";

        public double Loss(double[] estimatedValues, double[] actualValues)
        {
            return MathUtilities.CrossEntropy(estimatedValues, actualValues);
        }

        public double Derivative(double[] input, int current, int oneHot)
        {
            double target = current == oneHot ? 1.0 : 0.0;
            double result = input[current] - target;
            //double result = - 1.0 / (input[oneHot] + double.Epsilon);
            return result;
        }
    }
}
