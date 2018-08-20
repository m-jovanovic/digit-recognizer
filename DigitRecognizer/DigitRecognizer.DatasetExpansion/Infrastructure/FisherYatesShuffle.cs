using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitRecognizer.DatasetExpansion.Infrastructure
{
    /// <summary>
    /// Implements the Fisher-Yates shuffling algorithm.
    /// </summary>
    public class FisherYatesShuffle
    {
        /// <summary>
        /// Used in Shuffle(T).
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// Shuffle the array.
        /// </summary>
        /// <typeparam name="T">Array element type.</typeparam>
        /// <param name="array">Array to shuffle.</param>
        public static void Shuffle<T>(T[] array)
        {
            int n = array.Length;

            for (var i = 0; i < n; i++)
            {
                // Use Next on random instance with an argument.
                // The argument is an exclusive bound.
                // So we will not go past the end of the array.
                int r = i + Random.Next(n - i);

                T t = array[r];

                array[r] = array[i];

                array[i] = t;
            }
        }

        /// <summary>
        /// Shuffle the list.
        /// </summary>
        /// <typeparam name="T">Array element type.</typeparam>
        /// <param name="list">List to shuffle.</param>
        /// <returns>The shuffled list.</returns>
        public static List<T> Shuffle<T>(List<T> list)
        {
            T[] array = list.ToArray();

            Shuffle(array);

            return array.ToList();
        }
    }
}
