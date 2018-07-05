using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Interfaces.Functions;

namespace DigitRecognizer.MachineLearning.Optimizers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseOptimizer
    {
        /// <summary>
        /// 
        /// </summary>
        public IActivationFunction ActivationFunction { get; }

        /// <summary>
        /// 
        /// </summary>
        public ILossFunction LossFunction { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activationFunction"></param>
        /// <param name="lossFunction"></param>
        protected BaseOptimizer(IActivationFunction activationFunction, ILossFunction lossFunction)
        {
            Contracts.ValueNotNull(activationFunction, nameof(activationFunction));
            Contracts.ValueNotNull(lossFunction, nameof(lossFunction));

            ActivationFunction = activationFunction;
            LossFunction = lossFunction;
        }
    }
}
