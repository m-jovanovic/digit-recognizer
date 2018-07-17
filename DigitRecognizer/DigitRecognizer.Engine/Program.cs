using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Functions;
using DigitRecognizer.MachineLearning.Infrastructure;
using DigitRecognizer.MachineLearning.Optimization;

namespace DigitRecognizer.Engine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var optimizer = new GradientDescentOptimizer(new CrossEntropy());
            var nn = new NeuralNetwork();

            var layer1 = new NnLayer(784, 200, new Relu());
            var layer2 = new NnLayer(200, 60, new Relu());
            var layer3 = new NnLayer(60, 10, new Softmax());

            nn.AddLayer(layer1);
            nn.AddLayer(layer2);
            nn.AddLayer(layer3);

            double[][] m = VectorUtilities.CreateMatrix(200, 784);

            for (var i = 0; i < m.Length; i++)
            {
                for (var j = 0; j < m[0].Length; j++)
                {
                    m[i][j] = i * 0.001 + j * 0.0002;
                }
            }

            var prediction = nn.FeedForward(m);
            var onehot = new int[200];
            var error = optimizer.CalculateError(prediction[0], 8);
            var gradient = optimizer.CalculateOutputDerivative(prediction, onehot);

            nn.Backpropagate(gradient, onehot);
        }
    }
}
