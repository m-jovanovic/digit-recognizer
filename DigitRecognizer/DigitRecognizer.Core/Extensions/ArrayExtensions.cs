using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ArrayExtensions
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
    }
}
