using System;
using System.Collections.Generic;

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
            if (keepProbability < 0 || keepProbability > 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(keepProbability));
            }

            _keepProbability = keepProbability;

            _binomialDistributiion = new BinomialDistribution(keepProbability, new Random());
        }

        /// <summary>
        /// Randomly generates a list of dropout vectors of the specified sizes.
        /// </summary>
        /// <param name="sizes">The array of sizes.</param>
        /// <returns>The list of dropout vectors.</returns>
        public List<double[]> GenerateDropoutVectors(int[] sizes)
        {
            var result = new List<double[]>();

            foreach (int size in sizes)
            {
                var dropoutVector = new double[size];

                int[] binomial = _binomialDistributiion.Generate(size);

                // The reason to divide with the keep probability is so that the expected output of the network
                // during test time is not affacted by the expected output of the network during training time.

                for (var i = 0; i < size; i++)
                {
                    dropoutVector[i] = binomial[i] / _keepProbability;
                }

                result.Add(dropoutVector);
            }

            return result;
        }
    }
}
