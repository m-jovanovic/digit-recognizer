using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Extensions
{
    /// <summary>
    /// Contains extension methods for common vector operations.
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// Multiplies the specified matrices utilizing the <see cref="System.Threading.Tasks.Parallel"/> library.
        /// </summary>
        /// <param name="m1">The first matrix.</param>
        /// <param name="m2">The second matrix.</param>
        /// <returns>The result of the multiplication.</returns>
        public static double[][] Multiply(this double[][] m1, double[][] m2)
        {
            return VectorUtilities.Multiply(m1, m2);
        }

        /// <summary>
        /// Performs an element by element product of two identical size matrices.
        /// </summary>
        /// <param name="m1">The first matrix of the product.</param>
        /// <param name="m2">The second matrix of the product.</param>
        /// <returns>The Hadamard product of two matrices.</returns>
        public static double[][] HadamardProduct(this double[][] m1, double[][] m2)
        {
            return VectorUtilities.HadamardProduct(m1, m2);
        }

        /// <summary>
        /// Performs an element by element product of two identical size arrays.
        /// </summary>
        /// <param name="arr1">The first array of the product.</param>
        /// <param name="arr2">The second array of the product.</param>
        /// <returns>The Hadamard product of two arrays.</returns>
        public static double[] HadamardProduct(this double[] arr1, double[] arr2)
        {
            return VectorUtilities.HadamardProduct(arr1, arr2);
        }

        /// <summary>
        /// Performs the dot product multiplication of a matrix and an array.
        /// </summary>
        /// <param name="m">The matrix.</param>
        /// <param name="arr">The array.</param>
        /// <returns>The matrix.</returns>
        public static double[][] DotProduct(this double[][] m, double[] arr)
        {
            return VectorUtilities.DotProduct(m, arr);
        }

        /// <summary>
        /// Calculates the average of the specified matrix, column-wise.
        /// </summary>
        /// <param name="m">The matrix whose average is being calculated.</param>
        /// <returns>The average value of the matrix, per column.</returns>
        public static double[] Average(this double[][] m)
        {
            return VectorUtilities.Avg(m);
        }

        /// <summary>
        /// Calculates the average of the specified vector.
        /// </summary>
        /// <param name="arr">The array whose average is being calculated.</param>
        /// <returns>The average of the array.</returns>
        public static double Average(this double[] arr)
        {
            return VectorUtilities.Avg(arr);
        }

        /// <summary>
        /// Performs the elementwise addition of a matrix and a vector.s
        /// </summary>
        /// <param name="m">The matrix.</param>
        /// <param name="arr">The array.</param>
        /// <returns>The elementwise summ of the vector with the matrix rows.</returns>
        public static double[][] ElementwiseAdd(this double[][] m, double[] arr)
        {
            return VectorUtilities.ElementwiseAdd(m, arr);
        }

        /// <summary>
        /// Transposes the specified matrix.
        /// </summary>
        /// <param name="m">The matrix to be transposed.</param>
        /// <returns>The transposed matrix.</returns>
        public static double[][] Transpose(this double[][] m)
        {
            return VectorUtilities.Transpose(m);
        }

        /// <summary>
        /// Returns the maximum value of the array.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The max value.</returns>
        public static double Max(this double[] arr)
        {
            return VectorUtilities.Max(arr);
        }

        /// <summary>
        /// Returns the 0 based index of the maximum value of the array.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The 0 based index.</returns>
        public static int ArgMax(this double[] arr)
        {
            return VectorUtilities.ArgMax(arr);
        }

        /// <summary>
        /// Returns the 0 based index of the maximum value of the array.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The 0 based index.</returns>
        public static int ArgMax(this int[] arr)
        {
            return VectorUtilities.ArgMax(arr);
        }

        /// <summary>
        /// Adds two identical size matrices.
        /// </summary>
        /// <param name="m1">First matrix for addition.</param>
        /// <param name="m2">Second matrix for addition.</param>
        /// <returns>The sum of the matrices.</returns>
        public static double[][] Add(this double[][] m1, double[][] m2)
        {
            return VectorUtilities.Add(m1, m2);
        }

        /// <summary>
        /// Converts an array to a matrix with one element.
        /// </summary>
        /// <param name="arr">The array to be converted.</param>
        /// <returns>A jagged array a.k.a. matrix.</returns>
        public static double[][] AsMatrix(this double[] arr)
        {
            return VectorUtilities.AsMatrix(arr);
        }

        /// <summary>
        /// Sums the specified vector.
        /// </summary>
        /// <param name="arr">The array.</param>
        /// <returns>The sum of the vector.</returns>
        public static double Sum(this double[] arr)
        {
            return VectorUtilities.Sum(arr);
        }

        /// <summary>
        /// Returns a one-hot encoded array at the specified index.
        /// </summary>
        /// <param name="index">The 0 based index of the hot value.</param>
        /// <param name="length">The length of the resulting array.</param>
        /// <returns>The one-hot encoded array.</returns>
        public static double[] OneHot(this int index, int length)
        {
            return VectorUtilities.OneHot(index, length);
        }
    }
}
