using System;
using System.Collections.Generic;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Data;
using DigitRecognizer.MachineLearning.Infrastructure.Functions;

namespace DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork
{
    public class NeuralNetwork : INeuralNetwork
    {
        private readonly Core.Data.LinkedList<NnLayer> _layers;

        private double _learningRate;

        public double LearningRate
        {
            get => _learningRate;
            set => _learningRate = value;
        }

        public int NumberOfLayers => _layers.Count;

        public Core.Data.LinkedList<NnLayer> Layers => _layers;
        
        public List<double[][]> WeightedSumCache => _weightedSumCache.GetCache();

        public List<double[][]> ActivationCache => _activationCache.GetCache();

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
                result[i] = activationFunction.Derivative(cachedWeightedSum[i], oneHot[i].OneHot(colCount));
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
        public double[][] Decide(double[][] input)
        {
            if (input.Length > 1)
            {
                throw new NotSupportedException("Running more than 1 input through the network is not supported");
            }

            double[][] prediction = FeedForward(input);

            return prediction;
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
            for (var iter = 0; iter < outputError.Length; iter++)
            {
                double[][] currentError = outputError[iter].AsMatrix();

                Core.Data.LinkedListNode<NnLayer> currentLayer = _layers.Last;

                while (currentLayer != null)
                {
                    double[][] gradient = currentError;

                    // We don't multiply with the weighed sum derivative for the last layer in the network.
                    if (currentLayer.Depth < _layers.Count - 1)
                    {
                        double[][] wSumDeriv = WeightedSumDerivative(currentLayer.Value.ActivationFunction,
                            currentLayer.Depth, oneHot);

                        gradient = gradient.HadamardProduct(wSumDeriv[iter].AsMatrix());
                    }

                    //double[][] currentLayerError = gradients.Average().AsMatrix();

                    double[][] actvation = ActivationCache[currentLayer.Depth][iter].AsMatrix();

                    double[][] weightGradients = actvation.Transpose().Multiply(gradient);

                    currentLayer.Value.AdjustParameters(gradient, weightGradients, _learningRate);

                    currentError = currentLayer.Value.BackpropagateError(gradient);

                    currentLayer = currentLayer.Previous;
                }
            }

            //Core.Data.LinkedListNode<NnLayer> currentLayer = _layers.Last;
            //double[][] currentError = outputError;
            //while (currentLayer != null)
            //{
            //    double[][] gradients = currentError;

            //    if (currentLayer.Depth < _layers.Count - 1)
            //    {
            //        double[][] wSumDeriv = WeightedSumDerivative(currentLayer.Value.ActivationFunction,
            //            currentLayer.Depth, oneHot).Average().AsMatrix();

            //        gradients = gradients.HadamardProduct(wSumDeriv);
            //    }

            //    double[][] currentLayerError = gradients.Average().AsMatrix();

            //    //double[][] activations1 = _activationCache.GetValue(currentLayer.Depth);
            //    //double[][] weightGradients1 = VectorUtilities.CreateMatrix(currentLayer.Value.NumberOfInputs, currentLayer.Value.NumberOfOutputs);
            //    //for (var i = 0; i < activations1.Length; i++)
            //    //{
            //    //    double[][] deltaW = activations1[i].AsMatrix().Transpose().Multiply(gradients[i].AsMatrix());
            //    //    weightGradients1.Add(deltaW);
            //    //}
            //    //foreach (double[] gradRow in weightGradients1)
            //    //{
            //    //    for (var j = 0; j < weightGradients1[0].Length; j++)
            //    //    {
            //    //        gradRow[j] /= activations1.Length;
            //    //    }
            //    //}

            //    //double[][] averageActivation = _activationCache.GetValue(currentLayer.Depth).Average().AsMatrix();
            //    double[][] activations = _activationCache.GetValue(currentLayer.Depth);
            //    //double[][] weightGradients = averageActivation.Transpose().Multiply(currentLayerError);

            //    for (var i = 0; i < gradients.Length; i++)
            //    {
            //        double[][] weightGrad = activations[i].AsMatrix().Transpose().Multiply(gradients[i].AsMatrix());
            //        currentLayer.Value.AdjustParameters(gradients[i].AsMatrix(), weightGrad, _learningRate);
            //    }
            //    //currentLayer.Value.AdjustParameters(currentLayerError, weightGradients, _learningRate);

            //    currentError = currentLayer.Value.BackpropagateError(currentLayerError);

            //    currentLayer = currentLayer.Previous;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        public void AddLayer(IEnumerable<NnLayer> layers)
        {
            Contracts.ValueNotNull(layers, nameof(layers));

            foreach (NnLayer layer in layers)
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
