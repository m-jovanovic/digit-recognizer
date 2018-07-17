using DigitRecognizer.MachineLearning.Functions;

namespace DigitRecognizer.MachineLearning.Optimization
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
