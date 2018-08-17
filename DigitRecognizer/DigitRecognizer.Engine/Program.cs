using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.Data;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Pipeline;
using DigitRecognizer.MachineLearning.Infrastructure.Functions;
using DigitRecognizer.MachineLearning.Infrastructure.Initialization;
using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;
using DigitRecognizer.MachineLearning.Optimization;
using DigitRecognizer.MachineLearning.Optimization.LearningRateDecay;
using DigitRecognizer.MachineLearning.Providers;

namespace DigitRecognizer.Engine
{
    internal class Program
    {
        private static void Main()
        {
            var learningRate = 0.001;
            var epochs = 60;
            var regularizationFactor = 10.0;
            
            LearningPipeline pipeline = new LearningPipeline()
                .UseGradientClipping()
                .UseL2Regularization(regularizationFactor)
                //.UseLearningRateDecay(new StepDecay(learningRate, 0.5, 10))
                .SetWeightsInitializer(InitializerType.RandomInitialization)
                .SetEpochCount(epochs);

            var nn = new NeuralNetwork(learningRate);
            
            var layer1 = new NnLayer(784, 120, new Relu());
            var layer2 = new NnLayer(120, 50, new Relu());
            var layer3 = new NnLayer(50, 10, new Softmax());

            var layers = new List<NnLayer>
            {
                layer1,
                layer2, 
                layer3
            };

            nn.AddLayer(layers);

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
