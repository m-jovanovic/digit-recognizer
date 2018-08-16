using System;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Pipeline;

namespace DigitRecognizer.MachineLearning.Optimization.LearningRateDecay
{
    /// <summary>
    /// Implements the step decay learning rate scheduler.
    /// </summary>
    public class StepDecay : ILearningRateDecay
    {
        /// <summary>
        /// Reperesents the drop to be applied to the learning rate.
        /// </summary>
        private readonly double _drop;

        /// <summary>
        /// Reperesnts the number of epochs that need to elapse in order to drop the learning rate.
        /// </summary>
        private readonly double _epochDrop;
        
        /// <summary>
        /// The initial learning rate.
        /// </summary>
        private readonly double _initialLearningRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="StepDecay"/> class.
        /// </summary>
        /// <param name="initialLearningRate">The initial learning rate.</param>
        /// <param name="drop">The drop.</param>
        /// <param name="epochDrop">The epoch drop.</param>
        public StepDecay(double initialLearningRate, double drop, double epochDrop)
        {
            Contracts.ValueGreaterThanZero(initialLearningRate, nameof(initialLearningRate));
            Contracts.ValueGreaterThanZero(drop, nameof(drop));
            Contracts.ValueGreaterThanZero(epochDrop, nameof(epochDrop));

            _initialLearningRate = initialLearningRate;
            _drop = drop;
            _epochDrop = epochDrop;
        }

        /// <summary>
        /// Calculates the learning rate.
        /// </summary>
        /// <param name="learningRate">The current learning rate.</param>
        /// <returns>The new learning rate.</returns>
        public double DecayLearningRate(double learningRate)
        {
            learningRate = _initialLearningRate * Math.Pow(_drop, Math.Floor(PipelineSettings.Instance.CurrentEpoch / _epochDrop));

            return learningRate;
        }
    }
}
