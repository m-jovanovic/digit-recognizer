using System;
using DigitRecognizer.Core.Extensions;

namespace DigitRecognizer.MachineLearning.Infrastructure.Dropout
{
    /// <summary>
    /// Represents a generator for a binomial distribution.
    /// </summary>
    public class BinomialDistribution
    {
        private readonly double _p;
        private readonly Random _rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinomialDistribution"/> class.
        /// </summary>
        /// <param name="p">The probability of success.</param>
        /// <param name="rnd">The random number generator.</param>
        public BinomialDistribution(double p, Random rnd)
        {
            _p = p;
            _rnd = rnd;
        }

        /// <summary>
        /// Generates a random binomial distribution of the specified length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>The binomial distribution.</returns>
        public int[] Generate(int length)
        {
            double[] uniform = _rnd.NextDoubles(length);

            var binomial = new int[length];

            for (var i = 0; i < length; i++)
            {
                binomial[i] = uniform[i] < _p ? 1 : 0;
            }

            return binomial;
        }
    }
}
