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
        public ILossFunction LossFunction { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lossFunction"></param>
        protected BaseOptimizer(ILossFunction lossFunction)
        {
            Contracts.ValueNotNull(lossFunction, nameof(lossFunction));
            
            LossFunction = lossFunction;
        }
    }
}
