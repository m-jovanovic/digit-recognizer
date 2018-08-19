using System;

namespace DigitRecognizer.MachineLearning.Infrastructure.Dropout
{
    /// <summary>
    /// Implements the dropout regularization method.
    /// </summary>
    public class Dropout
    {
        private readonly BinomialDistribution _binomialDistributiion;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dropout"/> class.
        /// </summary>
        /// <param name="keepProbability">The keep probability.</param>
        public Dropout(double keepProbability)
        {
            if (keepProbability < 0 || keepProbability > 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(keepProbability));
            }

            _binomialDistributiion = new BinomialDistribution(keepProbability, new Random());
        }


    }
}
