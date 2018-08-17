using System;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.IO;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Functions;
using DigitRecognizer.MachineLearning.Serialization;

namespace DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork
{
    /// <summary>
    /// Represents a layer in the <see cref="NeuralNetwork"/>.
    /// </summary>
    public class NnLayer : INnSerializable
    {
        private readonly WeightMatrix _weightMatrix;
        private readonly BiasVector _biasVector;
        private IActivationFunction _activationFunction;
        private int _numberOfInputs;
        private int _numberOfOutputs;

        /// <summary>
        /// Initializes a new instance of the <see cref="NnLayer"/> class with the specified parameters.
        /// </summary>
        /// <param name="numberOfInputs">The number of inputs.</param>
        /// <param name="numberOfOutputs">The number of outputs.</param>
        /// <param name="activationFunction">The activation function.</param>
        public NnLayer(int numberOfInputs, int numberOfOutputs, IActivationFunction activationFunction)
        {
            Contracts.ValueGreaterThanZero(numberOfInputs, nameof(numberOfInputs));
            Contracts.ValueGreaterThanZero(numberOfOutputs, nameof(numberOfOutputs));
            Contracts.ValueNotNull(activationFunction, nameof(activationFunction));

            _numberOfInputs = numberOfInputs;
            _numberOfOutputs = numberOfOutputs;
            _activationFunction = activationFunction;

            _weightMatrix = new WeightMatrix(_numberOfInputs, _numberOfOutputs);

            _biasVector = new BiasVector(_numberOfOutputs);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NnLayer"/> class with the specified parameters.
        /// </summary>
        /// <param name="weightsRowCount">The number of row in the weight matrix.</param>
        /// <param name="weightsColCount">The number of columns in the weight matrix.</param>
        /// <param name="biasLength">The bias length.</param>
        /// <param name="data">The values of the weights and biases.</param>
        /// <param name="activationFunction">The activation function.</param>
        public NnLayer(int weightsRowCount, int weightsColCount, int biasLength, double[] data, IActivationFunction activationFunction)
        {
            Contracts.ValueGreaterThanZero(weightsRowCount, nameof(weightsRowCount));
            Contracts.ValueGreaterThanZero(weightsColCount, nameof(weightsColCount));
            Contracts.ValueGreaterThanZero(biasLength, nameof(biasLength));
            Contracts.ValueNotNull(activationFunction, nameof(activationFunction));

            _numberOfInputs = weightsRowCount;
            _numberOfOutputs = weightsColCount;
            
            _weightMatrix = new WeightMatrix(VectorUtilities.CreateMatrix(weightsRowCount, weightsColCount, data));

            var biasData = new double[biasLength];
            Buffer.BlockCopy(data, weightsRowCount * weightsColCount * sizeof(double), biasData, 0, biasLength * sizeof(double));
            _biasVector = new BiasVector(biasData);

            _activationFunction = activationFunction;
        }

        /// <summary>
        /// Gets or sets the number of inputs.
        /// </summary>
        public int NumberOfInputs
        {
            get => _numberOfInputs;
            set
            {
                Contracts.ValueGreaterThanZero(value, nameof(NumberOfInputs));
                _numberOfInputs = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of outputs.
        /// </summary>
        public int NumberOfOutputs
        {
            get => _numberOfOutputs;
            set
            {
                Contracts.ValueGreaterThanZero(value, nameof(NumberOfOutputs));
                _numberOfOutputs = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IActivationFunction"/>.
        /// </summary>
        public IActivationFunction ActivationFunction
        {
            get => _activationFunction;
            set
            {
                Contracts.ValueNotNull(value, nameof(ActivationFunction));
                _activationFunction = value;
            }
        }

        /// <summary>
        /// Gets the weights.
        /// </summary>
        public WeightMatrix WeightMatrix => _weightMatrix;

        /// <summary>
        /// Gets the bias.
        /// </summary>
        public BiasVector BiasVector => _biasVector;

        /// <summary>
        /// Calculates the weighted sum of the <see cref="NnLayer"/>.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The weighted sum.</returns>
        public double[][] WeightedSum(double[][] input)
        {
            double[][] weightedSum = input.Multiply(_weightMatrix).ElementwiseAdd(_biasVector);

            return weightedSum;
        }

        /// <summary>
        /// Calculates the output activation of the <see cref="NnLayer"/>.
        /// </summary>
        /// <param name="weightedSum">The weighted sum.</param>
        /// <returns>The activation.</returns>
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
        /// Backpropagates the error through the <see cref="NnLayer"/>.
        /// </summary>
        /// <param name="currentLayerError">The error of the current layer.</param>
        /// <returns>The new error.</returns>
        public double[][] BackpropagateError(double[][] currentLayerError)
        {
            return currentLayerError.Multiply(_weightMatrix.Weights.Transpose());
        }

        /// <summary>
        /// Serializes the <see cref="NnLayer"/>.
        /// </summary>
        /// <returns>The serialization context.</returns>
        public NnSerializationContext Serialize()
        {
            var contextInfo = new NnSerializationContextInfo(_numberOfInputs, _numberOfOutputs, _numberOfOutputs, _activationFunction.Name);

            int weightsLength = _numberOfInputs * _numberOfOutputs;
            int biasLength = _numberOfOutputs;

            var data = new double[weightsLength + biasLength];

            int weightsSizeInBytes = _weightMatrix.SizeInBytes;
            int biasSizeInBytes = _biasVector.SizeInBytes;

            Buffer.BlockCopy(_weightMatrix.Flattened, 0, data, 0, weightsSizeInBytes);
            Buffer.BlockCopy(_biasVector.Bias, 0, data, weightsSizeInBytes, biasSizeInBytes);

            var context = new NnSerializationContext(data, contextInfo);

            return context;
        }
    }
}
