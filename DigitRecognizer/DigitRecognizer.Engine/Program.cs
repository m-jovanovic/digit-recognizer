using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private static void Main()
        {
            LearningPipeline pipeline = new LearningPipeline()
                .UseGradientClipping()
                .SetWeightsInitializer(InitializerType.RandomInitialization)
                .UseL2Regularization()
                .SetRegularizationFactor(10.0)
                .SetEpochCount(25);

            var nn = new NeuralNetwork(0.0005);
            
            var layer1 = new NnLayer(784, 100, new Relu());
            var layer2 = new NnLayer(100, 10, new Softmax());

            nn.AddLayer(layer1);
            nn.AddLayer(layer2);

            var optimizer = new GradientDescentOptimizer(nn, new CrossEntropy());

            var provider = new BatchDataProvider(DirectoryHelper.TrainLabelsPath, DirectoryHelper.TrainImagesPath, 100);

            pipeline.Add(optimizer);

            pipeline.Add(nn);

            pipeline.Add(provider);

            PredictionModel model = pipeline.Run();

            var provider1 = new BatchDataProvider(DirectoryHelper.TestLabelsPath, DirectoryHelper.TestImagesPath, 10000);
            var acc = 0.0;

            MnistImageBatch data = provider1.GetData();

            List<double[]> predictions = data.Pixels.Select(pixels => model.Predict(pixels)).ToList();
            
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
