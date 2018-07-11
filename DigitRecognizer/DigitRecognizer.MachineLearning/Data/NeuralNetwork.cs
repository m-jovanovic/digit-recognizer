using System.Collections.Generic;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Interfaces.ML;

namespace DigitRecognizer.MachineLearning.Data
{
    public class NeuralNetwork : INeuralNetwork
    {
        private readonly Core.Data.LinkedList<NnLayer> _layers;

        private double _learningRate = 0.003;

        /// <summary>
        /// 
        /// </summary>
        public NeuralNetwork()
        {
            _layers = new Core.Data.LinkedList<NnLayer>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        public NeuralNetwork(NnLayer layer)
        {
            Contracts.ValueNotNull(layer, nameof(layer));
            
            _layers = new Core.Data.LinkedList<NnLayer>(layer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        public NeuralNetwork(IEnumerable<NnLayer> layers)
        {
            Contracts.ValueNotNull(layers, nameof(layers));
            
            _layers = new Core.Data.LinkedList<NnLayer>(layers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double[][] FeedForward(double[][] input)
        {
            Core.Data.LinkedListNode<NnLayer> currentLayer = _layers.First;
            double[][] output = input;
            while (currentLayer != null)
            {
                output = currentLayer.Value.FeedForward(output);
                currentLayer = currentLayer.Next;
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Backpropagate(double[][] outputError, int[] oneHot)
        {
            Core.Data.LinkedListNode<NnLayer> currentLayer = _layers.Last;
            double[][] currentError = outputError;
            while (currentLayer != null)
            {
                currentError = currentLayer.Value.BackPropagate(currentError, oneHot, _learningRate);
                currentLayer = currentLayer.Previous;
            }
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
