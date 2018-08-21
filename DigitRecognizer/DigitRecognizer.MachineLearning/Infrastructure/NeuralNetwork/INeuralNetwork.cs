using System.Collections.Generic;
using DigitRecognizer.MachineLearning.Pipeline;

namespace DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork
{
    /// <summary>
    /// Contains basic methods that a neural network should implement.
    /// </summary>
    public interface INeuralNetwork : ILearningPipelineNeuralNetworkModel
    {
        double LearningRate { get; set; }
        int NumberOfLayers { get; }

        Core.Data.LinkedList<NnLayer> Layers { get; }
        List<double[][]> WeightedSumCache { get; }
        List<double[][]> ActivationCache { get; }

        double[][] FeedForward(double[][] input);
        void AddLayer(IEnumerable<NnLayer> layers);
        void AddLayer(NnLayer layer);
    }
}
