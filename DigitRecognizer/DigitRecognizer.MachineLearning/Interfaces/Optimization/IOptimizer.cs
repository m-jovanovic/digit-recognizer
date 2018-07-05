using DigitRecognizer.MachineLearning.Data;
using DigitRecognizer.MachineLearning.Interfaces.Functions;

namespace DigitRecognizer.MachineLearning.Interfaces.Optimization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptimizer
    {
        IActivationFunction ActivationFunction { get; }
        ILossFunction LossFunction { get; }

        Gradient CalculateGradient(double[] activation, double[] prediction, int oneHot);
    }
}
