using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.IO;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Interfaces.Functions;
using DigitRecognizer.MachineLearning.Interfaces.ML;

namespace DigitRecognizer.MachineLearning.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class NnLayer : INnLayer
    {
        private readonly WeightMatrix _weights;
        private readonly BiasVector _bias;

        private readonly int _numberOfInputs;
        private readonly int _numberOfOutputs;

        private IActivationFunction _activationFunction;

        private double[][] _weightedSumCache;
        private double[][] _activationCache;

        /// <summary>
        /// 
        /// </summary>
        public IActivationFunction ActivationFunction
        {
            set
            {
                Contracts.ValueNotNull(value, nameof(value));

                _activationFunction = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfInputs"></param>
        /// <param name="numberOfOutputs"></param>
        public NnLayer(int numberOfInputs, int numberOfOutputs)
        {
            Contracts.ValueGreaterThanZero(numberOfInputs, nameof(numberOfInputs));
            Contracts.ValueGreaterThanZero(numberOfOutputs, nameof(numberOfOutputs));

            _numberOfInputs = numberOfInputs;
            _numberOfOutputs = numberOfOutputs;

            _weights = new WeightMatrix(_numberOfInputs, _numberOfOutputs);

            _bias = new BiasVector(_numberOfOutputs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weightsHeight"></param>
        /// <param name="weightsWidth"></param>
        /// <param name="biasLength"></param>
        /// <param name="data"></param>
        public NnLayer(int weightsHeight, int weightsWidth, int biasLength, double[] data)
        {
            _weights = new WeightMatrix(VectorUtilities.CreateMatrix(weightsHeight, weightsWidth, data));

            var biasData = new double[biasLength];
            Buffer.BlockCopy(data, weightsHeight * weightsWidth * sizeof(double), biasData, 0, biasLength * sizeof(double));
            _bias = new BiasVector(biasData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public NnFile Serialize()
        {
            var fileInfo = new NnFileInfo(_weights.Width, _weights.Height, _bias.Length);

            int weightsLength = _weights.Width * _weights.Height;
            int biasLength = _bias.Length;

            var data = new double[weightsLength + biasLength];
    
            Buffer.BlockCopy(_weights.Flattened, 0, data, 0, _weights.SizeInBytes);
            Buffer.BlockCopy(_bias.Flattened, 0, data, _weights.SizeInBytes, _bias.SizeInBytes);

            var file = new NnFile(data, fileInfo);

            return file;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double[][] FeedForward(double[][] input)
        {
            _activationCache = input;

            double[][] weightedSum = input.Multiply(_weights.Value).ElementwiseAdd(_bias.Value);

            _weightedSumCache = weightedSum;

            double[][] output = VectorUtilities.CreateMatrix(weightedSum.Length, weightedSum[0].Length);

            Parallel.For(0, output.Length, i =>
            {
                output[i] = _activationFunction.Activate(weightedSum[i]);
            });

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputError"></param>
        /// <param name="oneHot"></param>
        /// <param name="learningRate"></param>
        public double[][] BackPropagate(double[][] outputError, int[] oneHot, double learningRate)
        {
            double[][] wSumDerivative = WeightedSumDerivative(oneHot);

            double[][] gradients = VectorUtilities.CreateMatrix(_numberOfInputs, _numberOfOutputs);

            Parallel.For(0, _numberOfInputs, i => { gradients[i] = outputError[i].HadamardProduct(wSumDerivative[i]); });

            double[][] currentLayerError = { gradients.Average() };

            double[][] averageActivation = {_activationCache.Average()};

            double[][] weightGradients = averageActivation.Transpose().Multiply(currentLayerError);

            _weights.AdjustValue(weightGradients, learningRate);

            _bias.AdjustValue(currentLayerError, learningRate);

            return currentLayerError.Multiply(_weights.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oneHot"></param>
        /// <returns></returns>
        private double[][] WeightedSumDerivative(IReadOnlyList<int> oneHot)
        {
            int rowCount = _weightedSumCache.Length;
            int colCount = _weightedSumCache[0].Length;
            double[][] result = VectorUtilities.CreateMatrix(rowCount, colCount);

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    result[i][j] = _activationFunction.Derivative(_weightedSumCache[i], j, oneHot[i]);
                }
            }

            return result;
        }
    }
}
