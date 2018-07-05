namespace DigitRecognizer.MachineLearning.Interfaces.Functions
{
    /// <summary>
    /// Implement this interface on an activation function.
    /// </summary>
    public interface IActivationFunction : IDifferentiable, IFunction
    {
        double[] Activate(double[] arr);
    }
}
