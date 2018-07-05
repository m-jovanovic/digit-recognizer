namespace DigitRecognizer.MachineLearning.Data
{
    public class Gradient
    {
        public double[][] WeightGradient { get; }
        public double[][] BiasGradient { get; }

        public Gradient(double[][] weightGradient, double[][] biasGradient)
        {
            WeightGradient = weightGradient;
            BiasGradient = biasGradient;
        }
    }
}
