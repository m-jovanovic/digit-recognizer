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
        /// <param name="costFunction"></param>
        public GradientDescentOptimizer(ICostFunction costFunction) 
            : base(costFunction)
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
            double error = CostFunction.Cost(prediction, oneHot.OneHot(prediction.Length));

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
            double[] gradient = CostFunction.Derivative(prediction, oneHot.OneHot(length));

            const double threshold = 4.0;
            double l2Norm = MathUtilities.L2Norm(gradient);

            if (l2Norm > threshold)
            {
                for (var i = 0; i < length; i++)
                {
                    gradient[i] = gradient[i] * threshold / l2Norm;
                }
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
