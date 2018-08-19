using System.Collections.Generic;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Functions;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;

namespace DigitRecognizer.MachineLearning.Optimization.Optimizers
{
    /// <summary>
    /// Implements the gradient descent with momentum optimization algorithm.
    /// </summary>
    public class MomentumOptimizer : BaseOptimizer
    {
        private readonly double _momentum;

        private List<double[][]> _biasVelocities;
        private List<double[][]> _weightVelocities;

        /// <summary>
        /// initilazies a new instance of the <see cref="MomentumOptimizer"/> class.
        /// </summary>
        /// <param name="neuralNetwork">The neural network.</param>
        /// <param name="costFunction">The cost function.</param>
        /// <param name="momentum">The momentum value.</param>
        public MomentumOptimizer(INeuralNetwork neuralNetwork, ICostFunction costFunction, double momentum) : base(neuralNetwork, costFunction)
        {
            Contracts.ValueGreaterThanZero(momentum, nameof(momentum));
            Contracts.ValueNotNull(neuralNetwork, nameof(neuralNetwork));

            _momentum = momentum;

            InitializeVelocities(neuralNetwork);
        }
        
        /// <summary>
        /// Initializes the velocity matrices for the <see cref="INeuralNetwork"/>.
        /// </summary>
        /// <param name="neuralNetwork">The neural network.</param>
        private void InitializeVelocities(INeuralNetwork neuralNetwork)
        {
            _biasVelocities = new List<double[][]>();
            _weightVelocities = new List<double[][]>();

            foreach (NnLayer layer in neuralNetwork.Layers.ToList())
            {
                _biasVelocities.Add(VectorUtilities.CreateMatrix(1, layer.NumberOfOutputs));

                _weightVelocities.Add(VectorUtilities.CreateMatrix(layer.NumberOfInputs, layer.NumberOfOutputs));
            }
        }

        /// <summary>
        /// Adjusts the current parameters with specified gradient and learning rate.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="bDelta">The bias delta values.</param>
        /// <param name="wDelta">The weight delta values.</param>
        /// <param name="learningRate">The learning rate.</param>
        public override void AdjustParameters(Core.Data.LinkedListNode<NnLayer> node, double[][] bDelta, double[][] wDelta, double learningRate)
        {
            const int rowIndex = 0;
            for (var i = 0; i < _biasVelocities[node.Depth][0].Length; i++)
            {
                _biasVelocities[node.Depth][rowIndex][i] = _momentum * _biasVelocities[node.Depth][rowIndex][i] + bDelta[rowIndex][i];
            }

            for (var i = 0; i < _weightVelocities[node.Depth].Length; i++)
            {
                for (var j = 0; j < _weightVelocities[node.Depth][0].Length; j++)
                {
                    _weightVelocities[node.Depth][i][j] = _momentum * _weightVelocities[node.Depth][i][j] + wDelta[i][j];
                }
            }

            base.AdjustParameters(node, _biasVelocities[node.Depth], _weightVelocities[node.Depth], learningRate);
        }
    }
}
