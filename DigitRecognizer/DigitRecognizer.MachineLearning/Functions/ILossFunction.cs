namespace DigitRecognizer.MachineLearning.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILossFunction : IDifferentiable, IFunction
    {
        double Loss(double[] estimatedValues, double[] actualValues);
    }
}
