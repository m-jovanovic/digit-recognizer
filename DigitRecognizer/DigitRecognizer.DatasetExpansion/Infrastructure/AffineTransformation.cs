using System;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.DatasetExpansion.Infrastructure
{
    /// <summary>
    /// Contains methods for applying affine transformations.
    /// </summary>
    public static class AffineTransformation
    {
        private const int RowSize = 28;

        /// <summary>
        /// Translates the specified array in the specified direction.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <param name="x">The x offset.</param>
        /// <param name="y">The y offset.</param>
        /// <returns>The transalted array.</returns>
        public static byte[] Translate(byte[] arr, int x, int y)
        {
            return Translate(ToMatrix(arr), x, y);
        }

        /// <summary>
        /// Translates the specified matrix in the specified direction.
        /// </summary>
        /// <param name="m">The matrix.</param>
        /// <param name="x">The x offset.</param>
        /// <param name="y">The y offset.</param>
        /// <returns>The transalted matrix.</returns>
        public static byte[] Translate(byte[][] m, int x, int y)
        {
            byte[][] result = CreateMatrix(RowSize, RowSize);

            for (var i = 0; i < RowSize; i++)
            {
                for (var j = 0; j < RowSize; j++)
                {
                    result[Math.Abs((i + x) % RowSize)][Math.Abs((j + y) % RowSize)] = m[i][j];
                }
            }

            return ToArray(result);
        }

        /// <summary>
        /// Rotates the specified array for the specified degrees.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <param name="degrees">The degrees.</param>
        /// <returns>The rotated array.</returns>
        public static byte[] Rotate(byte[] arr, double degrees)
        {
            return Rotate(ToMatrix(arr), degrees);
        }

        /// <summary>
        /// Rotates the specified matrix for the specified degrees.
        /// </summary>
        /// <param name="m">The matrix.</param>
        /// <param name="degrees">The degrees.</param>
        /// <returns>The rotated matrix.</returns>
        public static byte[] Rotate(byte[][] m, double degrees)
        {
            byte[][] result = CreateMatrix(RowSize, RowSize);

            const int half = RowSize / 2;

            double sin = Math.Sin(degrees);
            double cos = Math.Cos(degrees);

            var xOffset = (int)Math.Round(half - cos * half - sin * half);
            var yOffset = (int)Math.Round(half - cos * half + sin * half);

            for (var i = 0; i < RowSize; i++)
            {
                for (var j = 0; j < RowSize; j++)
                {
                    int srcX = (int)Math.Round(i * cos + j * sin) + xOffset;

                    int srcY = (int)Math.Round(- sin * i + j * cos) + yOffset;

                    srcX = Math.Abs(srcX % RowSize);
                    srcY = Math.Abs(srcY % RowSize);

                    result[i][j] = m[srcX][srcY];
                }
            }

            return ToArray(result);
        }

        /// <summary>
        /// Creates the matrix of the specified dimensions.
        /// </summary>
        /// <param name="rowCount">The row count.</param>
        /// <param name="colCount">The col count.</param>
        /// <returns>The matrix.</returns>
        private static byte[][] CreateMatrix(int rowCount, int colCount)
        {
            var result = new byte[rowCount][];

            for (var i = 0; i < rowCount; i++)
            {
                result[i] = new byte[colCount];
            }

            return result;
        }

        /// <summary>
        /// Converts the specified array to a matrix in row major order.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The matrix.</returns>
        private static byte[][] ToMatrix(byte[] arr)
        {
            var result = new byte[RowSize][];

            for (var i = 0; i < RowSize; i++)
            {
                result[i] = new byte[RowSize];

                for (var j = 0; j < RowSize; j++)
                {
                    result[i][j] = arr[i * RowSize + j];
                }
            }

            return result;
        }

        /// <summary>
        /// Converts the specified matrix to an array in row major order.
        /// </summary>
        /// <param name="m">The matrix.</param>
        /// <returns>The array.</returns>
        private static byte[] ToArray(byte[][] m)
        {
            var result = new byte[RowSize * RowSize];

            for (var i = 0; i < RowSize; i++)
            {
                for (var j = 0; j < RowSize; j++)
                {
                    result[i * RowSize + j] = m[i][j];
                }
            }

            return result;
        }
    }
}
