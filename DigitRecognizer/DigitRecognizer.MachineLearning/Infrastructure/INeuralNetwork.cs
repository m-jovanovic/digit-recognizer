using System.Collections.Generic;
using DigitRecognizer.MachineLearning.Data;

namespace DigitRecognizer.MachineLearning.Interfaces.ML
{
    /// <summary>
    /// Contains basic methods that a neural network should implement.
    /// </summary>
    public interface INeuralNetwork
    {
        double[][] FeedForward(double[][] input);
        void Backpropagate(double[][] outputError, int[] oneHot);
        void AddLayer(IEnumerable<NnLayer> layers);
        void AddLayer(NnLayer layer);
    }
}
