using System.Threading.Tasks;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Functions;

namespace DigitRecognizer.MachineLearning.Optimization
{
    /// <summary>
    /// 
    /// </summary>
    public class GradientDescentOptimizer : BaseOptimizer, IOptimizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lossFunction"></param>
        public GradientDescentOptimizer(ILossFunction lossFunction) 
            : base(lossFunction)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prediction"></param>
        /// <param name="oneHot"></param>
        /// <returns></returns>
        public double CalculateError(double[] prediction, int oneHot)
        {
            double error = LossFunction.Loss(prediction, oneHot.OneHot(prediction.Length));

            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prediction"></param>
        /// <param name="oneHot"></param>
        /// <returns></returns>
        public double[] CalculateOutputDerivative(double[] prediction, int oneHot)
        {
            int length = prediction.Length;
            var gradient = new double[length];
            for (var i = 0; i < length; i++)
            {
                gradient[i] = LossFunction.Derivative(prediction, i, oneHot);
            }

            return gradient;
        }

        public double[][] CalculateOutputDerivative(double[][] predictions, int[] oneHots)
        {
            int length = predictions.Length;
            double[][] gradient = VectorUtilities.CreateMatrix(length, predictions[0].Length);
            Parallel.For(0, length, i =>
            {
                gradient[i] = CalculateOutputDerivative(predictions[i], oneHots[i]);
            });

            return gradient;
        }
    }
}
