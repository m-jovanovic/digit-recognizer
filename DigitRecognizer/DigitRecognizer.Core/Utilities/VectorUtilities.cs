using System;
using System.Threading.Tasks;

namespace DigitRecognizer.Core.Utilities
{
    /// <summary>
    /// Contains utility methods for commonly required vector operations.
    /// </summary>
    public static class VectorUtilities
    {
        /// <summary>
        /// Creates a new matrix of the specified dimensions.
        /// </summary>
        /// <param name="rowCount">The number of rows of the matrix.</param>
        /// <param name="colCount">The number of columns of the matrix.</param>
        /// <returns>The matrix of the specified size.</returns>
        public static double[][] CreateMatrix(int rowCount, int colCount)
        {
            Contracts.ValueGreaterThanZero(rowCount, nameof(rowCount));
            Contracts.ValueGreaterThanZero(colCount, nameof(colCount));

            var result = new double[rowCount][];

            for (var i = 0; i < rowCount; i++)
            {
                result[i] = new double[colCount];
            }

            return result;
        }

        /// <summary>
        /// Creates a new matrix of the specified dimensions.
        /// </summary>
        /// <param name="rowCount">The number of rows of the matrix.</param>
        /// <param name="colCount">The number of columns of the matrix.</param>
        /// <param name="flattenedData">The data for the matrix.</param>
        /// <returns>The matrix of the specified size.</returns>
        public static double[][] CreateMatrix(int rowCount, int colCount, double[] flattenedData)
        {
            Contracts.ValueGreaterThanZero(flattenedData.Length, nameof(flattenedData.Length));

            double[][] result = CreateMatrix(rowCount, colCount);

            var offset = 0;

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    result[i][j] = flattenedData[offset + j];
                }

                offset += colCount;
            }

            return result;
        }

        /// <summary>
        /// Converts an array to a matrix with one element.
        /// </summary>
        /// <param name="arr">The array to be converted.</param>
        /// <returns>A jagged array a.k.a. matrix.</returns>
        internal static double[][] AsMatrix(double[] arr)
        {
            double[][] result = { arr };

            return result;
        }

        /// <summary>
        /// Transposes the specified matrix.
        /// </summary>
        /// <param name="m">The matrix to be transposed.</param>
        /// <returns>The transposed matrix.</returns>
        internal static double[][] Transpose(double[][] m)
        {
            int rowCount = m.Length;
            int colCount = m[0].Length;

            double[][] result = CreateMatrix(colCount, rowCount);

            Parallel.For(0, colCount, col =>
            {
                for (var row = 0; row < rowCount; row++)
                {
                    result[col][row] = m[row][col];
                }
            });

            return result;
        }

        /// <summary>
        /// Performs the elementwise addition of two vectors.
        /// </summary>
        /// <param name="arr1">The first array.</param>
        /// <param name="arr2">The second array.</param>
        /// <returns>The elementwise sum of two vectors.</returns>
        internal static double[] ElementwiseAdd(double[] arr1, double[] arr2)
        {
            int length = arr1.Length;
            Contracts.ValuesMatch(length, arr2.Length, nameof(length));

            var result = new double[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = arr1[i] + arr2[i];
            }

            return result;
        }

        /// <summary>
        /// Performs the elementwise addition of a matrix and a vector.s
        /// </summary>
        /// <param name="m">The matrix.</param>
        /// <param name="arr">The array.</param>
        /// <returns>The elementwise summ of the vector with the matrix rows.</returns>
        internal static double[][] ElementwiseAdd(double[][] m, double[] arr)
        {
            int rowCount = m.Length;
            int colCount = m[0].Length;
            Contracts.ValuesMatch(colCount, arr.Length, nameof(arr.Length));

            double[][] result = CreateMatrix(rowCount, colCount);

            Parallel.For(0, rowCount, i =>
            {
                result[i] = ElementwiseAdd(m[i], arr);
            });

            return result;
        }

        /// <summary>
        /// Adds two identical size matrices.
        /// </summary>
        /// <param name="m1">First matrix for addition.</param>
        /// <param name="m2">Second matrix for addition.</param>
        /// <returns>The sum of the matrices.</returns>
        internal static double[][] Add(double[][] m1, double[][] m2)
        {
            int rowCount = m1.Length;
            int colCount = m1[0].Length;
            Contracts.ValuesMatch(rowCount, m2.Length, nameof(rowCount));
            Contracts.ValuesMatch(colCount, m2[0].Length, nameof(colCount));

            double[][] result = CreateMatrix(rowCount, colCount);
            Parallel.For(0, rowCount, i =>
            {
                for (var j = 0; j < colCount; j++)
                {
                    result[i][j] = m1[i][j] + m2[i][j];
                }
            });

            return result;
        }

        /// <summary>
        /// Multiplies the specified matrices utilizing the <see cref="Parallel"/> library.
        /// </summary>
        /// <param name="m1">The first matrix.</param>
        /// <param name="m2">The second matrix.</param>
        /// <returns>The result of the multiplication.</returns>
        internal static double[][] Multiply(double[][] m1, double[][] m2)
        {
            int colCount = m1[0].Length;
            int rowCount = m2.Length;
            Contracts.ValuesMatch(rowCount, colCount, nameof(rowCount));

            double[][] result = CreateMatrix(m1.Length, m2[0].Length);

            Parallel.For(0, result.Length, i => OptimizedMultiply(m1, m2, result, i));

            return result;
        }

        /// <summary>
        /// Peforms an optimized matrix multiplcation for the specified row.
        /// </summary>
        /// <param name="m1">The first matrix.</param>
        /// <param name="m2">The second matrix.</param>
        /// <param name="result">The result of the multiplication.</param>
        /// <param name="row">The current row being processed.</param>
        private static void OptimizedMultiply(double[][] m1, double[][] m2, double[][] result, int row)
        {
            double[] iRowA = m1[row];
            double[] iRowC = result[row];
            int kLength = m2.Length;
            int jLength = result[0].Length;

            for (var k = 0; k < kLength; k++)
            {
                double[] kRowB = m2[k];
                double ikA = iRowA[k];
                for (var j = 0; j < jLength; j++)
                {
                    iRowC[j] += ikA * kRowB[j];
                }
            }
        }

        /// <summary>
        /// Performs an element by element product of two identical size matrices.
        /// </summary>
        /// <param name="m1">The first matrix of the product.</param>
        /// <param name="m2">The second matrix of the product.</param>
        /// <returns>The Hadamard product of two matrices.</returns>
        internal static double[][] HadamardProduct(double[][] m1, double[][] m2)
        {
            int rowCount = m1.Length;
            int colCount = m1[0].Length;

            Contracts.ValuesMatch(rowCount, m2.Length, nameof(rowCount));
            Contracts.ValuesMatch(colCount, m2[0].Length, nameof(colCount));

            double[][] result = CreateMatrix(rowCount, colCount);

            Parallel.For(0, rowCount, i =>
            {
                result[i] = HadamardProduct(m1[i], m2[i]);
            });

            return result;
        }

        /// <summary>
        /// Performs an element by element product of two identical size arrays.
        /// </summary>
        /// <param name="arr1">The first array of the product.</param>
        /// <param name="arr2">The second array of the product.</param>
        /// <returns>The Hadamard product of two arrays.</returns>
        internal static double[] HadamardProduct(double[] arr1, double[] arr2)
        {
            int length = arr1.Length;
            Contracts.ValuesMatch(length, arr2.Length, nameof(length));

            var result = new double[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = arr1[i] * arr2[i];
            }

            return result;
        }

        /// <summary>
        /// Performs the dot product multiplication of a matrix and an array.
        /// </summary>
        /// <param name="m">The matrix.</param>
        /// <param name="arr">The array.</param>
        /// <returns>The matrix.</returns>
        internal static double[][] DotProduct(double[][] m, double[] arr)
        {
            int rowCount = m.Length;
            int colCount = m[0].Length;
            Contracts.ValuesMatch(colCount, arr.Length, nameof(arr.Length));

            double[][] result = CreateMatrix(rowCount, colCount);

            Parallel.For(0, rowCount, (i) =>
            {
                result[i] = HadamardProduct(m[i], arr);
            });

            return m;
        }

        /// <summary>
        /// Sums the specified vector.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The sum of the vector.</returns>
        internal static double Sum(double[] arr)
        {
            int length = arr.Length;
            var sum = 0.0;
            for (var i = 0; i < length; i++)
            {
                sum += arr[i];
            }

            return sum;
        }

        /// <summary>
        /// Calculates the average of the specified matrix, column-wise.
        /// </summary>
        /// <param name="m">The matrix whose average is being calculated.</param>
        /// <returns>The average value of the matrix, per column.</returns>
        internal static double[] Avg(double[][] m)
        {
            double[][] transposed = Transpose(m);

            int denominator = m.Length;
            int length = transposed.Length;
            var result = new double[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = Sum(transposed[i]) / denominator;
            }

            return result;
        }

        /// <summary>
        /// Calculates the average of the specified vector.
        /// </summary>
        /// <param name="arr">The array whose average is being calculated.</param>
        /// <returns>The average of the array.</returns>
        internal static double Avg(double[] arr)
        {
            var length = (double) arr.Length;

            double sum = Sum(arr);

            double avg = sum / length;

            return avg;
        }

        /// <summary>
        /// Returns a one-hot encoded array at the specified index.
        /// </summary>
        /// <param name="index">The 0 based index of the hot value.</param>
        /// <param name="length">The length of the resulting array.</param>
        /// <returns>The one-hot encoded array.</returns>
        internal static double[] OneHot(int index, int length)
        {
            Contracts.ValueGreaterThanZero(index, nameof(index));
            Contracts.ValueGreaterThanZero(length, nameof(length));
            Contracts.ValueWithinBounds(index, 0, length - 1, nameof(index));

            var result = new double[length];

            result[index] = 1;

            return result;
        }

        /// <summary>
        /// Returns the 0 based index of the maximum value of the array.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The 0 based index.</returns>
        internal static int ArgMax(int[] arr)
        {
            if (arr.Length == 0)
            {
                return -1;
            }

            int max = int.MinValue;
            int iMax = -1;
            int length = arr.Length;
            for (var i = 0; i < length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    iMax = i;
                }
            }

            return iMax;
        }

        /// <summary>
        /// Returns the 0 based index of the maximum value of the array.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The 0 based index.</returns>
        internal static int ArgMax(double[] arr)
        {
            int length = arr.Length;
            if (length == 0)
            {
                return -1;
            }

            double max = double.MinValue;
            int iMax = -1;
            for (var i = 0; i < length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    iMax = i;
                }
            }

            return iMax;
        }

        /// <summary>
        /// Returns the maximum value of the array.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The max value.</returns>
        internal static double Max(double[] arr)
        {
            double max = double.MinValue;

            int length = arr.Length;
            for (var i = 0; i < length; i++)
            {
                max = Math.Max(max, arr[i]);
            }

            return max;
        }

        /// <summary>
        /// Flattens a two dimensional vector to a one dimensional vector.
        /// </summary>
        /// <param name="m">The two dimensional vector.</param>
        /// <returns>The one dimensional vector.</returns>
        public static double[] Flatten(double[][] m)
        {
            int rowCount = m.Length;
            int colCount = m[0].Length;

            var result = new double[rowCount * colCount];
            var offset = 0;
            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    result[offset + j] = m[i][j];
                }

                offset += colCount;
            }

            return result;
        }
    }
}
