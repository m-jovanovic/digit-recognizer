using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.Data;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;
using DigitRecognizer.MachineLearning.Optimization.Optimizers;
using DigitRecognizer.MachineLearning.Providers;

namespace DigitRecognizer.MachineLearning.Pipeline
{
    /// <summary>
    /// Represents a configurable pipeline that can be used to train <see cref="NeuralNetwork"/> models.
    /// </summary>
    public class LearningPipeline : ICollection<ILearningPipelineItem>
    {
        private readonly List<ILearningPipelineItem> _items;

        /// <summary>
        /// Gets the singleton instance of <see cref="PipelineSettings"/>.
        /// </summary>
        internal PipelineSettings PipelineSettings => PipelineSettings.Instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="LearningPipeline"/>.
        /// </summary>
        public LearningPipeline()
        {
            _items = new List<ILearningPipelineItem>();
        }

        #region Methods

        /// <summary>
        /// Runs the learning pipeline and generates a <see cref="PredictionModel"/>.
        /// </summary>
        /// <returns>The prediction model.</returns>
        public PredictionModel Run()
        {
            ILearningPipelineDataLoader dataLoader = null;
            ILearningPipelineOptimizer optimizer = null;
            ILearningPipelineNeuralNetworkModel neuralNetworkModel = null;

            foreach (ILearningPipelineItem item in this)
            {
                switch (item)
                {
                    case ILearningPipelineDataLoader loader:
                        dataLoader = loader;
                        break;
                    case ILearningPipelineOptimizer opt:
                        optimizer = opt;
                        break;
                    case ILearningPipelineNeuralNetworkModel networkModel:
                        neuralNetworkModel = networkModel;
                        break;
                }
            }

            if (dataLoader == null)
            {
                throw new ArgumentNullException(nameof(dataLoader));
            }

            if (optimizer == null)
            {
                throw new ArgumentNullException(nameof(optimizer));
            }

            if (neuralNetworkModel == null)
            {
                throw new ArgumentNullException(nameof(neuralNetworkModel));
            }

            // Indicate the start of training.
            PipelineSettings.IsPipelingRunning = true;
            PipelineSettings.CurrentIteration = 0;

            for (var epoch = 0; epoch < PipelineSettings.EpochCount; epoch++)
            {
                PipelineSettings.CurrentEpoch = epoch + 1;

                ConsoleUtility.WriteLine($"Current epoch: {PipelineSettings.CurrentEpoch}");

                if (PipelineSettings.UseLearningRateDecay)
                {
                    ApplyLearningRateScheduler(neuralNetworkModel);
                }

                for (var i = 0; i < PipelineSettings.TrainingIterationsCount; i++)
                {
                    PipelineSettings.CurrentIteration++;

                    if (PipelineSettings.CanPerformDropout)
                    {
                        PipelineSettings.DropoutVectors = PipelineSettings.Dropout.GenerateDropoutVectors(PipelineSettings.HiddenLayerSizes);
                    }

                    // A training iteration is constited of three steeps:

                    // 1. Load data
                    var data = (MnistImageBatch)dataLoader.LoadData();

                    // 2. Feedforward step
                    double[][] prediction = neuralNetworkModel.Predict(data.Pixels);

                    // 3. Backpropagation step
                    optimizer.Optimize(prediction, data.Labels);
                }
            }

            // Indicate the end of training.
            PipelineSettings.IsPipelingRunning = false;

            return new PredictionModel((INeuralNetwork)neuralNetworkModel);
        }
        
        /// <summary>
        /// Applies the learning rate scheduler if the <see cref="LearningPipeline"/> is configured to use learning rate decay.
        /// </summary>
        /// <param name="neuralNetworkModel">The neural network model.</param>
        private void ApplyLearningRateScheduler(ILearningPipelineNeuralNetworkModel neuralNetworkModel)
        {
            Contracts.ValueNotNull(PipelineSettings.LearningRateScheduler, nameof(PipelineSettings.LearningRateScheduler));

            var nerualNetwork = (INeuralNetwork)neuralNetworkModel;

            nerualNetwork.LearningRate = PipelineSettings.LearningRateScheduler.DecayLearningRate(nerualNetwork.LearningRate);
        }

        #endregion

        #region ICollection implementation

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="LearningPipeline"/>.
        /// </summary>
        /// <returns>The enumerator of the collection.</returns>
        public IEnumerator<ILearningPipelineItem> GetEnumerator() => _items.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="LearningPipeline"/>.
        /// </summary>
        /// <returns>The enumerator of the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Adds an <see cref="ILearningPipelineItem"/> to the end of the <see cref="LearningPipeline"/>.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(ILearningPipelineItem item)
        {
            _items.Add(item);

            // Run a check to see if everything is alright.
            ValidateConfiguration();

            switch (item)
            {
                case ILearningPipelineDataLoader dataLoader:
                    ConfigurePipeline(dataLoader);
                    break;
                case ILearningPipelineOptimizer optimizer:
                    ConfigurePipeline(optimizer);
                    break;
                case ILearningPipelineNeuralNetworkModel neuralNetworkModel:
                    ConfigurePipeline(neuralNetworkModel);
                    break;
            }
        }

        /// <summary>
        /// Removes all elements from <see cref="LearningPipeline"/>.
        /// </summary>
        public void Clear() => _items.Clear();

        /// <summary>
        /// Determines whether the specified <see cref="ILearningPipelineItem"/> is in the <see cref="LearningPipeline"/>
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>true if the item is in the collection.</returns>
        public bool Contains(ILearningPipelineItem item) => _items.Contains(item);

        /// <summary>
        /// Copies the specified arrray startinng at the specified index.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">The index.</param>
        public void CopyTo(ILearningPipelineItem[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        /// <summary>
        /// Removes the first occurrence of the specified <see cref="ILearningPipelineItem"/> from the <see cref="LearningPipeline"/>.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>true if the item is removed.</returns>
        public bool Remove(ILearningPipelineItem item) => _items.Remove(item);

        /// <summary>
        /// Gets the number of elements contained in the <see cref="LearningPipeline"/>.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Return a value indicating if the <see cref="LearningPipeline"/> is readonly.
        /// </summary>
        public bool IsReadOnly => true;

        #endregion

        #region Configuration

        /// <summary>
        /// Configure the pipeline settings.
        /// </summary>
        /// <param name="dataLoader">The data loader.</param>
        private void ConfigurePipeline(ILearningPipelineDataLoader dataLoader)
        {
            var provider = (BatchDataProvider)dataLoader;

            this.SetBatchSize(provider.BatchSize);

            int datasetSize = DetermineDatasetSize(provider.LabelFilename);

            this.SetDatasetSize(datasetSize);
        }

        /// <summary>
        /// Configures the piepline settings.
        /// </summary>
        /// <param name="optimizer">The optimizer.</param>
        private void ConfigurePipeline(ILearningPipelineOptimizer optimizer)
        {
            var opt = (IOptimizer)optimizer;

            // TODO: Finish this method.
        }

        /// <summary>
        /// Configures the piepline settings.
        /// </summary>
        /// <param name="neuralNetworkModel">The neural network model.</param>
        private void ConfigurePipeline(ILearningPipelineNeuralNetworkModel neuralNetworkModel)
        {
            var network = (INeuralNetwork)neuralNetworkModel;

            var sizes = new int[network.NumberOfLayers - 1];

            List<NnLayer> layers = network.Layers.ToList();

            for (var i = 1; i < network.NumberOfLayers; i++)
            {
                sizes[i - 1] = layers[i].NumberOfInputs;
            }
            
            this.SetHiddenLayerSizes(sizes);
        }

        /// <summary>
        /// Determines the dataset size based on the filename.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        /// <returns>The dataset size.</returns>
        private int DetermineDatasetSize(string filename)
        {
            using (var binaryReader = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read)))
            {
                // Skip the magic number.
                binaryReader.ReadInt32();

                // The integer is in MSB format,
                byte[] msbBytes = binaryReader.ReadBytes(sizeof(int));

                byte[] lsbBytes = msbBytes.Reverse().ToArray();

                int datasetSize = BitConverter.ToInt32(lsbBytes, 0);

                return datasetSize;
            }
        }

        /// <summary>
        /// Validates the current pipeline configuration.
        /// </summary>
        private void ValidateConfiguration()
        {
            if (Count > 3)
            {
                throw new NotSupportedException("The learning pipeline contains to many items");
            }

            var loaderCount = 0;
            var optimizerCount = 0;
            var networkModelCount = 0;
            foreach (ILearningPipelineItem item in this)
            {
                switch (item)
                {
                    case ILearningPipelineDataLoader _:
                        loaderCount++;
                        break;
                    case ILearningPipelineOptimizer _:
                        optimizerCount++;
                        break;
                    case ILearningPipelineNeuralNetworkModel _:
                        networkModelCount++;
                        break;
                }
            }

            if (loaderCount + optimizerCount + networkModelCount != Count)
            {
                throw new NotSupportedException("The learning pipeline contains duplicate items");
            }
        }

        #endregion
    }
}
