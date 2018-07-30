using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Functions;

namespace DigitRecognizer.MachineLearning.Optimization
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseOptimizer
    {
        /// <summary>
        /// 
        /// </summary>
        public ICostFunction CostFunction { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="costFunction"></param>
        protected BaseOptimizer(ICostFunction costFunction)
        {
            Contracts.ValueNotNull(costFunction, nameof(costFunction));
            
            CostFunction = costFunction;
        }
    }
}
