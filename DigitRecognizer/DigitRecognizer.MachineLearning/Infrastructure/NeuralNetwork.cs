using System.Collections.Generic;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Functions;

namespace DigitRecognizer.MachineLearning.Infrastructure
{
    public class NeuralNetwork : INeuralNetwork
    {
        private readonly Core.Data.LinkedList<NnLayer> _layers;

        private double _learningRate;

        public int NumberOfLayers => _layers.Count;

        private readonly CalculationCache _weightedSumCache;
        private readonly CalculationCache _activationCache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activationFunction"></param>
        /// <param name="nodeDepth"></param>
        /// <param name="oneHot"></param>
        /// <returns></returns>
        public double[][] WeightedSumDerivative(IActivationFunction activationFunction, int nodeDepth, int[] oneHot)
        {
            double[][] cachedWeightedSum = _weightedSumCache.GetValue(nodeDepth);

            int rowCount = cachedWeightedSum.Length;
            int colCount = cachedWeightedSum[0].Length;
            double[][] result = VectorUtilities.CreateMatrix(rowCount, colCount);

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    result[i][j] = activationFunction.Derivative(cachedWeightedSum[i], j, oneHot[i]);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public NeuralNetwork(double learningRate)
        {
            _learningRate = learningRate;
            _layers = new Core.Data.LinkedList<NnLayer>();
            _weightedSumCache = new CalculationCache();
            _activationCache = new CalculationCache();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="learningRate"></param>
        public NeuralNetwork(NnLayer layer, double learningRate)
        {
            _learningRate = learningRate;
            Contracts.ValueNotNull(layer, nameof(layer));
            
            _layers = new Core.Data.LinkedList<NnLayer>(layer);
            _weightedSumCache = new CalculationCache();
            _activationCache = new CalculationCache();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        /// <param name="learningRate"></param>
        public NeuralNetwork(IEnumerable<NnLayer> layers, double learningRate)
        {
            _learningRate = learningRate;
            Contracts.ValueNotNull(layers, nameof(layers));
            
            _layers = new Core.Data.LinkedList<NnLayer>(layers);
            _weightedSumCache = new CalculationCache();
            _activationCache = new CalculationCache();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double[][] FeedForward(double[][] input)
        {
            Core.Data.LinkedListNode<NnLayer> currentLayer = _layers.First;
            double[][] output = input;
            while (currentLayer != null)
            {
                _activationCache.SetValue(output, currentLayer.Depth);

                double[][] weightedSum = currentLayer.Value.WeightedSum(output);

                _weightedSumCache.SetValue(weightedSum, currentLayer.Depth);

                output = currentLayer.Value.OutputActivation(weightedSum);

                currentLayer = currentLayer.Next;
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Backpropagate(double[][] outputError, int[] oneHot)
        {
            Core.Data.LinkedListNode<NnLayer> currentLayer = _layers.Last;
            double[][] currentError = outputError;
            while (currentLayer != null)
            {
                double[][] wSumDeriv = WeightedSumDerivative(currentLayer.Value.ActivationFunction,
                    currentLayer.Depth, oneHot);

                double[][] gradients = currentError.HadamardProduct(wSumDeriv);

                double[][] currentLayerError = gradients.Average().AsMatrix();

                double[][] averageActivation = _activationCache.GetValue(currentLayer.Depth).Average().AsMatrix();

                double[][] weightGradients = averageActivation.Transpose().Multiply(currentLayerError);

                currentLayer.Value.AdjustParameters(currentLayerError, weightGradients, _learningRate);

                currentError = currentLayer.Value.BackpropagateError(gradients);

                currentLayer = currentLayer.Previous;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        public void AddLayer(IEnumerable<NnLayer> layers)
        {
            foreach (var layer in layers)
            {
                AddLayer(layer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        public void AddLayer(NnLayer layer)
        {
            Contracts.ValueNotNull(layer, nameof(layer));

            _layers.AddLast(layer);
        }
    }
}
