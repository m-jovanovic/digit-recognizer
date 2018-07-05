using System;
using System.Linq;

namespace DigitRecognizer.Core.Extensions
{
    /// <summary>
    /// Provides extensions methods for working with the <see cref="double"/> struct.
    /// </summary>
    public static class DoubleExtensions
    {
        private const double DoubleNorm = 255.0D;
        private const double DoubleOffsetNorm = 127.5D;

        /// <summary>
        /// Returns a double, parsed from the specified byte.
        /// </summary>
        /// <param name="value">The value that will be converted to a double.</param>
        /// <returns>A double.</returns>
        public static double FromByte(byte value)
        {
            return value;
        }

        /// <summary>
        /// Returns a double in the range [0, 1], parsed from the specified byte.
        /// </summary>
        /// <param name="value">The value that will be parsed then normalized.</param>
        /// <returns>A double in the range [0,1].</returns>
        public static double FromByteNormalized(byte value)
        {
            return FromByte(value) / DoubleNorm;
        }

        /// <summary>
        /// Returns a double in the range [-0.5, 0.5], parsed from the specified byte.
        /// </summary>
        /// <param name="value">The value that will be parsed then normalized.</param>
        /// <returns>A double in the range [-0.5,0.5].</returns>
        public static double FromByteOffset(byte value)
        {
            return FromByte(value) / DoubleOffsetNorm - 1;
        }

        /// <summary>
        /// Gets the bytes of the double array, utilizing the <see cref="BitConverter"/> class.
        /// </summary>
        /// <param name="array">The array of doubles that will be converted to bytes.</param>
        /// <returns>A byte array.</returns>
        public static byte[] GetBytes(this double[] array)
        {
            var result = array.SelectMany(BitConverter.GetBytes).ToArray();

            return result;
        }

        /// <summary>
        /// Gets the bytes of the double array, utilizing the <see cref="Buffer"/> class.
        /// </summary>
        /// <param name="array">The array of doubles that will be converted to bytes.</param>
        /// <returns>A byte array.</returns>
        public static byte[] ToBytes(this double[] array)
        {
            var result = new byte[array.Length * sizeof(double)];

            Buffer.BlockCopy(array, 0, result, 0, result.Length);

            return result;
        }
    }
}
