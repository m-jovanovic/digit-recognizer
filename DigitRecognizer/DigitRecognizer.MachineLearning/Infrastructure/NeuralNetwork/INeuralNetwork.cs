using System.Collections.Generic;

namespace DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork
{
    /// <summary>
    /// Contains basic methods that a neural network should implement.
    /// </summary>
    public interface INeuralNetwork
    {
        double LearningRate { get; set; }
        Core.Data.LinkedList<NnLayer> Layers { get; }
        List<double[][]> WeightedSumCache { get; }
        List<double[][]> ActivationCache { get; }

        double[][] FeedForward(double[][] input);
        void Backpropagate(double[][] outputError, int[] oneHot);
        void AddLayer(IEnumerable<NnLayer> layers);
        void AddLayer(NnLayer layer);
    }
}
