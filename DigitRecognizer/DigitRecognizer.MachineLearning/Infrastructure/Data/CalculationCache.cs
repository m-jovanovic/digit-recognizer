using System.Collections.Generic;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Infrastructure.Data
{
    /// <summary>
    /// Represents a simple cache for storing values that need to be saved.
    /// </summary>
    public class CalculationCache
    {
        private readonly List<double[][]> _cache;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationCache"/> class.
        /// </summary>
        public CalculationCache()
        {
            _cache = new List<double[][]>();
        }

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <returns></returns>
        public List<double[][]> GetCache()
        {
            return _cache;
        }

        /// <summary>
        /// Sets the specified value at the specified index.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="index">The index.</param>
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
        /// Addes the specified value to the cache.
        /// </summary>
        /// <param name="value"></param>
        private void Add(double[][] value)
        {
            Contracts.ValueNotNull(value, nameof(value));

            _cache.Add(value);
        }
    }
}
