using System;

namespace DigitRecognizer.MachineLearning.Infrastructure.Dropout
{
    /// <summary>
    /// Implements the dropout regularization method.
    /// </summary>
    public class Dropout
    {
        private readonly double _keepProbability;
        private readonly BinomialDistribution _binomialDistributiion;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dropout"/> class.
        /// </summary>
        /// <param name="keepProbability">The keep probability.</param>
        public Dropout(double keepProbability)
        {
            _keepProbability = keepProbability;

            _binomialDistributiion = new BinomialDistribution(_keepProbability, new Random());
        }


    }
}
