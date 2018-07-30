using System;
using System.IO;
using DigitRecognizer.MachineLearning.Functions;
using DigitRecognizer.MachineLearning.Infrastructure;
using DigitRecognizer.MachineLearning.Optimization;
using DigitRecognizer.MachineLearning.Providers;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Serialization;

namespace DigitRecognizer.Engine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var optimizer = new GradientDescentOptimizer(new CrossEntropy());
            var nn = new NeuralNetwork(1.0);
            //var deserializer = new NnDeserializer();
            //var serilaztionContext = deserializer.Deseralize("../../../Models/model0.nn");
            //var layer1 = new NnLayer(784, 200, new LeakyRelu());
            //var layer2 = new NnLayer(200, 60, new LeakyRelu());
            //var layer3 = new NnLayer(60, 10, new Softmax());
            var layer1 = new NnLayer(784, 10, new Softmax());
            //var layer2 = new NnLayer(30, 10, new Softmax());

            nn.AddLayer(layer1);
            //nn.AddLayer(layer2);
            //nn.AddLayer(layer3);

            var provider = new BatchDataProvider(
                "../../../Dataset/train-labels.idx1-ubyte",
                "../../../Dataset/train-images.idx3-ubyte", 1);

            var iter = 60000;
            for (var epoch = 0; epoch < 2; epoch++)
            {
                if (epoch > 0 && epoch % 10 == 0)
                {
                    nn.LearningRate *= 0.1;
                }
                for (var i = 0; i < iter; i++)
                {
                    var data = provider.GetData();

                    var predictions = nn.FeedForward(NormalizePixels(data.Pixels));
                    if (i % 100 == 0)
                    {
                        var error = 0.0;
                        for (var j = 0; j < predictions.Length; j++)
                        {
                            error += optimizer.CalculateError(predictions[j], data.Labels[j]);
                        }

                        error /= predictions.Length;

                        Console.WriteLine($"Eror for iteration {epoch * 60000 + i}: {error}");
                    }

                    var err = optimizer.CalculateOutputDerivative(predictions, data.Labels);

                    nn.Backpropagate(err, data.Labels);
                }
            }

            var provider1 = new BatchDataProvider(DirectoryHelper.TestLabelsPath, DirectoryHelper.TestImagesPath, 100);
            double acc = 0.0;
            for (var i = 0; i < 100; i++)
            {
                var data = provider1.GetData();

                var predictions = nn.FeedForward(NormalizePixels(data.Pixels));
                for(var j = 0; j < 100; j++)
                {
                    if (data.Labels[j] == predictions[j].ArgMax())
                        acc++;
                }
            }
            acc /= 10000.0;

            Console.WriteLine($"Accuracy on the test data is: {acc:P2}");

            var serializer = new NnSerializer();
            string basePath = Path.GetFullPath(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) +
                                               DirectoryHelper.ModelsFolder);
            string modelName = $"{Guid.NewGuid()}-{acc:N5}.nn";
            string filename = $"{basePath}/{modelName}";
            serializer.Serialize(filename, nn.Layers);
            Console.ReadKey();
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
