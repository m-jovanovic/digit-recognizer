using System;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.IO;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Functions;
using DigitRecognizer.MachineLearning.Serialization;

namespace DigitRecognizer.MachineLearning.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class NnLayer : INnSerializable
    {
        private readonly WeightMatrix _weights;
        private readonly BiasVector _bias;

        private readonly int _numberOfInputs;
        private readonly int _numberOfOutputs;

        private readonly IActivationFunction _activationFunction;

        /// <summary>
        /// 
        /// </summary>
        public IActivationFunction ActivationFunction => _activationFunction;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfInputs"></param>
        /// <param name="numberOfOutputs"></param>
        /// <param name="activationFunction"></param>
        public NnLayer(int numberOfInputs, int numberOfOutputs, IActivationFunction activationFunction)
        {
            Contracts.ValueGreaterThanZero(numberOfInputs, nameof(numberOfInputs));
            Contracts.ValueGreaterThanZero(numberOfOutputs, nameof(numberOfOutputs));
            Contracts.ValueNotNull(activationFunction, nameof(activationFunction));

            _numberOfInputs = numberOfInputs;
            _numberOfOutputs = numberOfOutputs;
            _activationFunction = activationFunction;

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
        /// <param name="input"></param>
        /// <returns></returns>
        public double[][] WeightedSum(double[][] input)
        {
            double[][] weightedSum = input.Multiply(_weights).ElementwiseAdd(_bias);

            return weightedSum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weightedSum"></param>
        /// <returns></returns>
        public double[][] OutputActivation(double[][] weightedSum)
        {
            double[][] output = VectorUtilities.CreateMatrix(weightedSum.Length, weightedSum[0].Length);

            for (var i = 0; i < output.Length; i++)
            {
                output[i] = _activationFunction.Activate(weightedSum[i]);
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="biasGradients"></param>
        /// <param name="weightGradients"></param>
        /// <param name="learningRate"></param>
        public void AdjustParameters(double[][] biasGradients, double[][] weightGradients, double learningRate)
        {
            _weights.AdjustValue(weightGradients, learningRate);

            _bias.AdjustValue(biasGradients, learningRate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentLayerError"></param>
        /// <returns></returns>
        public double[][] BackpropagateError(double[][] currentLayerError)
        {
            return currentLayerError.Multiply(_weights.Value.Transpose());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public NnSerializationContext Serialize()
        {
            var fileInfo = new NnSerializationContextInfo(_numberOfInputs, _numberOfOutputs, _bias.Length);

            int weightsLength = _numberOfInputs * _numberOfOutputs;
            int biasLength = _bias.Length;

            var data = new double[weightsLength + biasLength];

            int weightsSizeInBytes = _weights.SizeInBytes;
            int biasSizeInBytes = _bias.SizeInBytes;

            Buffer.BlockCopy(_weights.Flattened, 0, data, 0, weightsSizeInBytes);
            Buffer.BlockCopy(_bias.Flattened, 0, data, weightsSizeInBytes, biasSizeInBytes);

            var file = new NnSerializationContext(data, fileInfo);

            return file;
        }
    }
}
