namespace DigitRecognizer.MachineLearning.Interfaces.Functions
{
    /// <summary>
    /// This interface should be implemented by all differentiable functions.
    /// </summary>
    public interface IDifferentiable
    {
        double Derivative(double[] input, int current = 0, int oneHot = 0);
    }
}
