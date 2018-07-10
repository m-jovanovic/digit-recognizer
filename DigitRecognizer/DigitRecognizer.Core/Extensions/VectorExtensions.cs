using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static double[][] Multiply(this double[][] m1, double[][] m2)
        {
            return VectorUtilities.Multiply(m1, m2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static double[][] HadamardProduct(this double[][] m1, double[][] m2)
        {
            return VectorUtilities.HadamardProduct(m1, m2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public static double[] HadamardProduct(this double[] arr1, double[] arr2)
        {
            return VectorUtilities.HadamardProduct(arr1, arr2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static double[] Average(this double[][] m)
        {
            return VectorUtilities.Avg(m);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double Average(this double[] arr)
        {
            return VectorUtilities.Avg(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double[][] ElementwiseAdd(this double[][] m, double[] arr)
        {
            return VectorUtilities.ElementwiseAdd(m, arr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static double[][] Transpose(this double[][] m)
        {
            return VectorUtilities.Transpose(m);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double Max(this double[] arr)
        {
            return VectorUtilities.Max(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int ArgMax(this double[] arr)
        {
            return VectorUtilities.ArgMax(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int ArgMax(this int[] arr)
        {
            return VectorUtilities.ArgMax(arr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static double[][] Add(this double[][] m1, double[][] m2)
        {
            return VectorUtilities.Add(m1, m2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static double[][] AsMatrix(this double[] arr)
        {
            return VectorUtilities.AsMatrix(arr);
        }
    }
}
