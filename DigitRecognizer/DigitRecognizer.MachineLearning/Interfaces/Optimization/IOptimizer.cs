using DigitRecognizer.MachineLearning.Interfaces.Functions;

namespace DigitRecognizer.MachineLearning.Interfaces.Optimization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptimizer
    {
        ILossFunction LossFunction { get; }

        double CalculateError(double[] prediction, int oneHot);
        double[] CalculateOutputDerivative(double[] prediction, int oneHot);
        double[][] CalculateOutputDerivative(double[][] predictions, int[] oneHots);
    }
}
