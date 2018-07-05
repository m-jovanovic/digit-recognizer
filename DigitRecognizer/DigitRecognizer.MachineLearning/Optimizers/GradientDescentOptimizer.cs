using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Data;
using DigitRecognizer.MachineLearning.Interfaces.Functions;
using DigitRecognizer.MachineLearning.Interfaces.Optimization;

namespace DigitRecognizer.MachineLearning.Optimizers
{
    /// <summary>
    /// 
    /// </summary>
    public class GradientDescentOptimizer : BaseOptimizer, IOptimizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="activationFunction"></param>
        /// <param name="lossFunction"></param>
        public GradientDescentOptimizer(IActivationFunction activationFunction, ILossFunction lossFunction) : base(activationFunction, lossFunction)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activation"></param>
        /// <param name="prediction"></param>
        /// <param name="oneHot"></param>
        /// <returns></returns>
        public Gradient CalculateGradient(double[] activation, double[] prediction, int oneHot)
        {
            var biasGrad = VectorUtilities.CreateMatrix(1, prediction.Length);

            var rowIndex = 0;
            var length = biasGrad[0].Length;
            for(var i =0; i < length; i++)
            {
                biasGrad[rowIndex][i] = LossFunction.Derivative(prediction, i, oneHot) *
                              ActivationFunction.Derivative(prediction, i, oneHot);
            }

            var weightGrad = VectorUtilities.CreateMatrix(activation.Length, prediction.Length);
            
            var height = activation.Length;
            var width = prediction.Length;
            for (var j = 0; j < width; j++)
            {
                for (var i = 0; i < height; i++)
                {
                    weightGrad[i][j] = LossFunction.Derivative(prediction, j, oneHot)
                                       * ActivationFunction.Derivative(prediction, j, oneHot) * activation[i];
                }
            }

            return new Gradient(weightGrad, biasGrad);
        }
    }
}
