using System;
using System.Threading.Tasks;

namespace DigitRecognizer.Core.Utilities
{
    /// <summary>
    /// Contains utility functions for commonly required vector operations.
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
        /// 
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="flattenedData"></param>
        /// <returns></returns>
        public static double[][] CreateMatrix(int rowCount, int colCount, double[] flattenedData)
        {
            Contracts.ValueGreaterThanZero(flattenedData.Length, nameof(flattenedData.Length));

            var result = CreateMatrix(rowCount, colCount);

            var offset = 0;

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    result[i][j] = flattenedData[offset + j];
                }

                offset += rowCount;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double[][] CreateMatrix(double[] arr)
        {
            var result = new[]
            {
                arr
            };

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static double[][] Transpose(double[][] m)
        {
            var rowCount = m.Length;
            var colCount = m[0].Length;

            var result = CreateMatrix(colCount, rowCount);

            Parallel.For(0, colCount, i =>
            {
                for (var j = 0; j < rowCount; j++)
                {
                    result[i][j] = m[j][i];
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
        public static double[] ElementwiseAdd(double[] arr1, double[] arr2)
        {
            var length = arr1.Length;
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
        public static double[][] ElementwiseAdd(double[][] m, double[] arr)
        {
            var rowCount = m.Length;
            var colCount = m[0].Length;
            Contracts.ValuesMatch(colCount, arr.Length, nameof(arr.Length));

            var result = CreateMatrix(rowCount, colCount);

            Parallel.For(0, rowCount, i =>
            {
                result[i] = ElementwiseAdd(m[i], arr);
            });

            return result;
        }

        /// <summary>
        /// Multiplies the specified matrices utilizing the <see cref="Parallel"/> library.
        /// </summary>
        /// <param name="m1">The first matrix.</param>
        /// <param name="m2">The second matrix.</param>
        /// <returns>The result of the multiplication.</returns>
        public static double[][] Multiply(double[][] m1, double[][] m2)
        {
            var colCount = m1[0].Length;
            var rowCount = m2.Length;
            Contracts.ValuesMatch(rowCount, colCount, nameof(rowCount));

            var result = CreateMatrix(m1.Length, m2[0].Length);

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
            var iRowA = m1[row];
            var iRowC = result[row];
            var kLength = m2.Length;
            var jLength = result[0].Length;

            for (var k = 0; k < kLength; k++)
            {
                var kRowB = m2[k];
                var ikA = iRowA[k];
                for (var j = 0; j < jLength; j++)
                {
                    iRowC[j] += ikA * kRowB[j];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static double[][] HadamardProduct(double[][] m1, double[][] m2)
        {
            var rowCount = m1.Length;
            var colCount = m1[0].Length;

            Contracts.ValuesMatch(rowCount, m2.Length, nameof(rowCount));
            Contracts.ValuesMatch(colCount, m2[0].Length, nameof(colCount));

            var result = CreateMatrix(rowCount, colCount);

            Parallel.For(0, rowCount, i =>
            {
                result[i] = Product(m1[i], m2[i]);
            });

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public static double[] Product(double[] arr1, double[] arr2)
        {
            var length = arr1.Length;
            Contracts.ValuesMatch(length, arr2.Length, nameof(length));

            var result = new double[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = arr1[i] * arr2[i];
            }

            return result;
        }

        /// <summary>
        /// Sums the specified vector.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The sum of the vector.</returns>
        public static double Sum(double[] arr)
        {
            var sum = 0.0;
            var length = arr.Length;

            for (var i = 0; i < length; i++)
            {
                sum += arr[i];
            }

            return sum;
        }

        public static double[] Avg(double[][] m)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the average of the specified vector.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The average of the array.</returns>
        public static double Avg(double[] arr)
        {
            var length = (double) arr.Length;

            var sum = Sum(arr);

            var avg = sum / length;

            return avg;
        }

        /// <summary>
        /// Returns a one-hot encoded array at the specified index.
        /// </summary>
        /// <param name="index">The 0 based index of the hot value.</param>
        /// <param name="length">The length of the resulting array.</param>
        /// <returns>The one-hot encoded array.</returns>
        public static int[] OneHot(int index, int length)
        {
            Contracts.ValueGreaterThanZero(index, nameof(index));
            Contracts.ValueGreaterThanZero(length, nameof(length));
            Contracts.ValueWithinBounds(index, 0, length - 1, nameof(index));

            var result = new int[length];

            result[index] = 1;

            return result;
        }

        /// <summary>
        /// Returns the 0 based index of the maximum value of the array.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The 0 based index.</returns>
        public static int ArgMax(int[] arr)
        {
            if (arr.Length == 0)
            {
                return -1;
            }

            var max = int.MinValue;
            var iMax = -1;
            var length = arr.Length;
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
        public static int ArgMax(double[] arr)
        {
            var length = arr.Length;
            if (length == 0)
            {
                return -1;
            }

            var max = double.MinValue;
            var iMax = -1;
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
        public static double Max(double[] arr)
        {
            var max = double.MinValue;

            var length = arr.Length;
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
            var rowCount = m.Length;
            var colCount = m[0].Length;

            var result = new double[rowCount * colCount];
            var offset = 0;
            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    result[offset + j] = m[i][j];
                }

                offset += rowCount;
            }

            return result;
        }
    }
}
