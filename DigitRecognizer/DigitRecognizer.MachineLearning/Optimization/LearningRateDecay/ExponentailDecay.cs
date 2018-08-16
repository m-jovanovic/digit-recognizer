using System;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Pipeline;

namespace DigitRecognizer.MachineLearning.Optimization.LearningRateDecay
{
    /// <summary>
    /// Implements the exponential decay learning rate scheduler.
    /// </summary>
    public class ExponentailDecay : ILearningRateDecay
    {
        /// <summary>
        /// Represents the decay rate to be applied to the learning rate.
        /// </summary>
        private readonly double _decayRate;

        /// <summary>
        /// The initial learning rate.
        /// </summary>
        private readonly double _initialLearningRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentailDecay"/> class.
        /// </summary>
        /// <param name="initialLearningRate">The initial learning rate.</param>
        /// <param name="decayRate">The decay rate.</param>
        public ExponentailDecay(double initialLearningRate, double decayRate)
        {
            Contracts.ValueGreaterThanZero(initialLearningRate, nameof(initialLearningRate));
            Contracts.ValueGreaterThanZero(decayRate, nameof(decayRate));

            _initialLearningRate = initialLearningRate;
            _decayRate = decayRate;
        }

        /// <summary>
        /// Calculates the learning rate.
        /// </summary>
        /// <param name="learningRate">The current learning rate.</param>
        /// <returns>The new learning rate.</returns>
        public double DecayLearningRate(double learningRate)
        {
            learningRate = _initialLearningRate * Math.Exp(-_decayRate * PipelineSettings.Instance.CurrentIteration);

            return learningRate;
        }
    }
}
