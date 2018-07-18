using System;
using DigitRecognizer.MachineLearning.Functions;
using DigitRecognizer.MachineLearning.Infrastructure;
using DigitRecognizer.MachineLearning.Optimization;
using DigitRecognizer.MachineLearning.Providers;

namespace DigitRecognizer.Engine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var optimizer = new GradientDescentOptimizer(new CrossEntropy());
            var nn = new NeuralNetwork(0.003);

            //var layer1 = new NnLayer(784, 200, new LeakyRelu());
            //var layer2 = new NnLayer(200, 60, new LeakyRelu());
            //var layer3 = new NnLayer(60, 10, new Softmax());
            var layer1 = new NnLayer(784, 10, new Softmax());

            nn.AddLayer(layer1);
            //nn.AddLayer(layer2);
            //nn.AddLayer(layer3);

            var provider = new BatchDataProvider(
                "C:\\Users\\ING\\source\\repos\\ML\\digit-recognizer\\DigitRecognizer\\Dataset\\train-labels.idx1-ubyte",
                "C:\\Users\\ING\\source\\repos\\ML\\digit-recognizer\\DigitRecognizer\\Dataset\\train-images.idx3-ubyte", 100);

            for (var i = 0; i < 6000; i++)
            {
                var data = provider.GetData();
                
                var predictions = nn.FeedForward(NormalizePixels(data.Pixels));

                var error = 0.0;
                for (var j = 0; j < predictions.Length; j++)
                {
                    error += optimizer.CalculateError(predictions[j], data.Labels[j]);
                }

                error /= predictions.Length;

                Console.WriteLine($"Eror for iteration {i}: {error}");

                var err = optimizer.CalculateOutputDerivative(predictions, data.Labels);

                nn.Backpropagate(err, data.Labels);
            }
        }

        static double[][] NormalizePixels(double[][] pixels)
        {
            for (var i = 0; i < pixels.Length; i++)
            {
                for (var j = 0; j < pixels[0].Length; j++)
                {
                    pixels[i][j] = pixels[i][j] / 255d;
                }
            }

            return pixels;
        }
    }
}
