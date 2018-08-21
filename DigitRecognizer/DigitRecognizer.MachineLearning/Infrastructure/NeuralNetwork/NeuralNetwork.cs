using System.Collections.Generic;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Data;
using DigitRecognizer.MachineLearning.Pipeline;

namespace DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork
{
    /// <summary>
    /// Represents a neural network with fully connected layers.
    /// </summary>
    public class NeuralNetwork : INeuralNetwork
    {
        private readonly Core.Data.LinkedList<NnLayer> _layers;
        private double _learningRate;
        private readonly CalculationCache _weightedSumCache;
        private readonly CalculationCache _activationCache;

        /// <summary>
        /// Gets or sets the learning rate.
        /// </summary>
        public double LearningRate
        {
            get => _learningRate;
            set => _learningRate = value;
        }

        /// <summary>
        /// Gets the number of layers.
        /// </summary>
        public int NumberOfLayers => _layers.Count;

        /// <summary>
        /// Gets the layers of the neural network.
        /// </summary>
        public Core.Data.LinkedList<NnLayer> Layers => _layers;
        
        /// <summary>
        /// Gets the weighted sum cache.
        /// </summary>
        public List<double[][]> WeightedSumCache => _weightedSumCache.GetCache();

        /// <summary>
        /// Gets the activatio cache.
        /// </summary>
        public List<double[][]> ActivationCache => _activationCache.GetCache();

        /// <summary>
        /// Initializes a new instance of the <see cref="NeuralNetwork"/> class.
        /// </summary>
        /// <param name="learningRate">The learning rate.</param>
        public NeuralNetwork(double learningRate)
        {
            _learningRate = learningRate;
            _layers = new Core.Data.LinkedList<NnLayer>();
            _weightedSumCache = new CalculationCache();
            _activationCache = new CalculationCache();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeuralNetwork"/> class with the specified layers.
        /// </summary>
        /// <param name="layers">The collection of <see cref="NnLayer"/> objects.</param>
        /// <param name="learningRate">The learning rate.</param>
        public NeuralNetwork(IEnumerable<NnLayer> layers, double learningRate)
        {
            _learningRate = learningRate;
            Contracts.ValueNotNull(layers, nameof(layers));
            
            _layers = new Core.Data.LinkedList<NnLayer>(layers);
            _weightedSumCache = new CalculationCache();
            _activationCache = new CalculationCache();
        }

        /// <summary>
        /// Generates a prediction based on the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The prediction.</returns>
        public double[][] Predict(double[][] input)
        {
            return FeedForward(input);
        }

        /// <summary>
        /// Feeds the input values forward thorugh the network layers.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The output activation of the last layer.</returns>
        public double[][] FeedForward(double[][] input)
        {
            Core.Data.LinkedListNode<NnLayer> currentLayer = _layers.First;
            double[][] output = input;
            while (currentLayer != null)
            {
                _activationCache.SetValue(output, currentLayer.Depth);

                double[][] weightedSum = currentLayer.Value.WeightedSum(output);

                if (PipelineSettings.Instance.CanPerformDropout && currentLayer.Next != null)
                {
                    double[] dropoutVector = PipelineSettings.Instance.DropoutVectors[currentLayer.Depth]; 

                    weightedSum = weightedSum.DotProduct(dropoutVector);
                }

                _weightedSumCache.SetValue(weightedSum, currentLayer.Depth);

                output = currentLayer.Value.OutputActivation(weightedSum);

                currentLayer = currentLayer.Next;
            }

            return output;
        }

        /// <summary>
        /// Adds the specified collection of <see cref="NnLayer"/> objects to the <see cref="NeuralNetwork"/>.
        /// </summary>
        /// <param name="layers">The collection of <see cref="NnLayer"/> objects.</param>
        public void AddLayer(IEnumerable<NnLayer> layers)
        {
            Contracts.ValueNotNull(layers, nameof(layers));

            foreach (NnLayer layer in layers)
            {
                AddLayer(layer);
            }
        }

        /// <summary>
        /// Adds the specified layer to the <see cref="NeuralNetwork"/>.
        /// </summary>
        /// <param name="layer">The <see cref="NnLayer"/> object.</param>
        public void AddLayer(NnLayer layer)
        {
            Contracts.ValueNotNull(layer, nameof(layer));

            _layers.AddLast(layer);
        }
    }
}
