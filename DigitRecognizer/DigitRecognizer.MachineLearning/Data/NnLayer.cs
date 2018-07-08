using System;
using System.Threading.Tasks;
using DigitRecognizer.Core.DataStructures;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.InputOutput;
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

        private double[][] WeightedSumDerivative()
        {
            var result = VectorUtilities.CreateMatrix(_weightedSumCache.Length, _weightedSumCache[0].Length);

            for (var i = 0; i < _weightedSumCache.Length; i++)
            {
                for (var j = 0; j < _weightedSumCache[0].Length; j++)
                {
                    result[i][j] = _activationFunction.Derivative(_weightedSumCache[i], j, j);
                }
            }

            return result;
        }

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

            var weightsLength = _weights.Width * _weights.Height;
            var biasLength = _bias.Length;

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
            //_activationCache = input;

            //var weightedSum = input.Multiply(_weights.Value).ElementwiseAdd(_bias.Value);

            //_weightedSumCache = weightedSum;

            //var activation = VectorUtilities.CreateMatrix(weightedSum.Length, weightedSum[0].Length);

            //Parallel.For(0, activation.Length, i =>
            //{
            //    activation[i] = _activationFunction.Activate(weightedSum[i]);
            //});

            //return Next == null ? activation : Next?.Value.FeedForward(activation);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wDerivative"></param>
        /// <param name="bDerivative"></param>
        /// <param name="learningRate"></param>
        public void BackPropagate(double[][] wDerivative, double[][] bDerivative, double learningRate)
        {
            //var weightedSumDerivative = WeightedSumDerivative();
            
            //var currWeightErr = Next == null ? 
            //    wDerivative : 
            //    Next.Value._weights.Value.Transpose().Multiply(wDerivative);

            //currWeightErr = currWeightErr.HadamardProduct(weightedSumDerivative);

            //var currBiasErr = Next == null ? 
            //    bDerivative : 
            //    Next.Value._weights.Value.Transpose().Multiply(bDerivative);

            //var weightGradient = Previous.Value._activationCache.Transpose().Multiply(currWeightErr);
            //var biasGradient = currBiasErr;

            //_weights.AdjustValue(weightGradient, learningRate);

            //_bias.AdjustValue(biasGradient, learningRate);

            //Previous?.Value?.BackPropagate(currWeightErr, currBiasErr, learningRate);
        }
    }
}
