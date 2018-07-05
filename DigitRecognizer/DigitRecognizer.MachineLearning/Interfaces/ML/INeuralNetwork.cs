using DigitRecognizer.MachineLearning.Data;

namespace DigitRecognizer.MachineLearning.Interfaces.ML
{
    /// <summary>
    /// 
    /// </summary>
    public interface INeuralNetwork
    {
        double[][] FeedForward(double[][] input);

        void AddLayer(NnLayer layer);
    }
}
