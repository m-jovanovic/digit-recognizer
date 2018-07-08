using System.Collections.Generic;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Interfaces.ML;

namespace DigitRecognizer.MachineLearning.Data
{
    public class NeuralNetwork : INeuralNetwork
    {
        private readonly Core.DataStructures.LinkedList<NnLayer> _layers;

        /// <summary>
        /// 
        /// </summary>
        public NeuralNetwork()
        {
            _layers = new Core.DataStructures.LinkedList<NnLayer>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        public NeuralNetwork(NnLayer layer)
        {
            Contracts.ValueNotNull(layer, nameof(layer));
            
            _layers = new Core.DataStructures.LinkedList<NnLayer>(layer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        public NeuralNetwork(IEnumerable<NnLayer> layers)
        {
            Contracts.ValueNotNull(layers, nameof(layers));
            
            _layers = new Core.DataStructures.LinkedList<NnLayer>(layers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double[][] FeedForward(double[][] input)
        {
            var result = _layers.First.Value.FeedForward(input);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        public void AddLayer(NnLayer layer)
        {
            Contracts.ValueNotNull(layer, nameof(layer));

            _layers.AddLast(layer);
        }
    }
}
