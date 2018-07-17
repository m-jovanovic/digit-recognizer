using System.Collections.Generic;

namespace DigitRecognizer.MachineLearning.Infrastructure
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
