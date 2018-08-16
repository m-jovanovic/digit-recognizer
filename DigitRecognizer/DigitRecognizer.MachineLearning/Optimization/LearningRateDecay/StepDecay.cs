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
        /// Initializes a new instance of the <see cref="StepDecay"/> class.
        /// </summary>
        /// <param name="drop">The drop.</param>
        /// <param name="epochDrop">The epochs drop.</param>
        public StepDecay(double drop, double epochDrop)
        {
            Contracts.ValueGreaterThanZero(drop, nameof(drop));
            Contracts.ValueGreaterThanZero(epochDrop, nameof(epochDrop));

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
            learningRate *= _drop * Math.Floor(PipelineSettings.Instance.CurrentEpoch / _epochDrop);

            return learningRate;
        }
    }
}
