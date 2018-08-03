using System;
using System.Collections.Generic;
using System.IO;
using DigitRecognizer.Core.Data;
using DigitRecognizer.MachineLearning.Optimization;
using DigitRecognizer.MachineLearning.Providers;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Functions;
using DigitRecognizer.MachineLearning.Infrastructure.Initialization;
using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;
using DigitRecognizer.MachineLearning.Pipeline;

namespace DigitRecognizer.Engine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            LearningPipeline pipeline = new LearningPipeline()
                .UseGradientClipping()
                .SetWeightsInitializer(InitializerType.RandomInitialization)
                .SetEpochCount(1);

            var nn = new NeuralNetwork(0.001);

            var optimizer = new GradientDescentOptimizer(nn, new CrossEntropy());

            var provider = new BatchDataProvider(DirectoryHelper.TrainLabelsPath, DirectoryHelper.TrainImagesPath, 100);

            var layer1 = new NnLayer(784, 200, new Relu());
            var layer2 = new NnLayer(200, 10, new Softmax());

            nn.AddLayer(layer1);
            nn.AddLayer(layer2);

            pipeline.Add(optimizer);

            pipeline.Add(nn);

            pipeline.Add(provider);

            PredictionModel model = pipeline.Run();

            var provider1 = new BatchDataProvider(DirectoryHelper.TestLabelsPath, DirectoryHelper.TestImagesPath, 10000);
            var acc = 0.0;
            var predictions = new List<double[]>();
            
            MnistImageBatch data = provider1.GetData();

            foreach (double[] pixels in data.Pixels)
            {
                double[] output = model.Predict(pixels);

                predictions.Add(output);
            }

            for (var i = 0; i < data.Labels.Length; i++)
            {
                if (data.Labels[i] == predictions[i].ArgMax())
                {
                    acc++;
                }
            }
            
            acc /= 10000.0;

            Console.WriteLine($"Accuracy on the test data is: {acc:P2}");

            string basePath = Path.GetFullPath(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) +
                                               DirectoryHelper.ModelsFolder);

            string modelName = $"{Guid.NewGuid()}-{acc:N5}.nn";

            string filename = $"{basePath}/{modelName}";

            model.Save(filename);

            Console.ReadKey();
        }
    }
}
