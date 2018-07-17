using System.Collections.Generic;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CalculationCache
    {
        private readonly List<double[][]> _cache;
        
        /// <summary>
        /// 
        /// </summary>
        public CalculationCache()
        {
            _cache = new List<double[][]>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double[][] GetValue(int index)
        {
            Contracts.ValueWithinBounds(index, 0, _cache.Count, nameof(index));
            
            return _cache[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public void SetValue(double[][] value, int index)
        {
            Contracts.ValueNotNull(value, nameof(value));
            Contracts.ValueWithinBounds(index, 0, _cache.Count, nameof(index));

            if (index > _cache.Count - 1)
            {
                Add(value);
            }

            _cache[index] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void Add(double[][] value)
        {
            Contracts.ValueNotNull(value, nameof(value));

            _cache.Add(value);
        }
    }
}
