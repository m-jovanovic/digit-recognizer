namespace DigitRecognizer.MachineLearning.Interfaces.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILossFunction : IDifferentiable, IFunction
    {
        double Loss(double[] estimatedValues, double[] actualValues);
    }
}
