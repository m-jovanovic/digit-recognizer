using System.Collections;
using System.Collections.Generic;
using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;

namespace DigitRecognizer.MachineLearning.Pipeline
{
    public class LearningPipeline : ICollection<ILearningPipelineItem>
    {
        private readonly List<ILearningPipelineItem> _items;
        private int _iterationCount;

        public LearningPipeline()
        {
            _items = new List<ILearningPipelineItem>();
        }

        public IEnumerator<ILearningPipelineItem> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(ILearningPipelineItem item) => _items.Add(item);

        public void Clear() => _items.Clear();

        public bool Contains(ILearningPipelineItem item) => _items.Contains(item);

        public void CopyTo(ILearningPipelineItem[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

        public bool Remove(ILearningPipelineItem item) => _items.Remove(item);

        public int Count => _items.Count;

        public bool IsReadOnly => true;

        public PredictionModel Run()
        {
            ILearningPipelineDataLoader dataLoader = null;
            ILearningPipelineNeuralNetworkModel nnModel = null;
            ILearningPipelineOptimizer optimizer = null;

            foreach (var item in this)
            {
                if (item is ILearningPipelineDataLoader loader)
                {
                    dataLoader = loader;
                }
                else if (item is ILearningPipelineNeuralNetworkModel model)
                {
                    nnModel = model;
                }
                else if (item is ILearningPipelineOptimizer opt)
                {
                    optimizer = opt;
                }
            }

            var neuralNetwork = new NeuralNetwork(1);

            for (var i = 0; i < _iterationCount; i++)
            {
                var data = dataLoader?.LoadData();


            }

            return new PredictionModel(neuralNetwork);
        }
    }
}
