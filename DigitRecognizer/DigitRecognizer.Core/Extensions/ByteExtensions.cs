using System;

namespace DigitRecognizer.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for working with <see cref="byte"/> struct.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Converts the specified byte array to an array of doubles, utilizing the <see cref="Buffer"/> class.
        /// </summary>
        /// <param name="array">The array of bytes that will be converted to doubles.</param>
        /// <returns>A double array.</returns>
        public static double[] ToDoubles(this byte[] array)
        {
            if (array.Length == 0)
            {
                return default(double[]);
            }

            var result = new double[array.Length / sizeof(double)];
            Buffer.BlockCopy(array, 0, result, 0, array.Length);
            return result;
        }
    }
}
