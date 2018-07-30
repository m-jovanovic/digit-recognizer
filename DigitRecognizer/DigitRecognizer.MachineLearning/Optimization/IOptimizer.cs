using DigitRecognizer.MachineLearning.Functions;

namespace DigitRecognizer.MachineLearning.Optimization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptimizer
    {
        ICostFunction CostFunction { get; }

        double CalculateError(double[] prediction, int oneHot);
        double[] CalculateOutputDerivative(double[] prediction, int oneHot);
        double[][] CalculateOutputDerivative(double[][] predictions, int[] oneHots);
    }
}
