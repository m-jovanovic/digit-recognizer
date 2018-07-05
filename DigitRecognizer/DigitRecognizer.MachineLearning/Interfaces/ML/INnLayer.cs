using DigitRecognizer.MachineLearning.Interfaces.Functions;
using DigitRecognizer.MachineLearning.Interfaces.InputOutput;

namespace DigitRecognizer.MachineLearning.Interfaces.ML
{
    /// <summary>
    /// 
    /// </summary>
    public interface INnLayer : INnSerializable
    {
        IActivationFunction ActivationFunction { set; }

        double[][] FeedForward(double[][] input);
        void BackPropagate(double[][] wDerivative, double[][] bDerivative, double learningRate);
    }
}
