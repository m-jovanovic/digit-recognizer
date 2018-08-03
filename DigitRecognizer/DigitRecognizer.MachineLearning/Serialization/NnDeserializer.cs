using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.IO;
using DigitRecognizer.MachineLearning.Infrastructure.Factories;
using DigitRecognizer.MachineLearning.Infrastructure.Functions;
using DigitRecognizer.MachineLearning.Infrastructure.NeuralNetwork;

namespace DigitRecognizer.MachineLearning.Serialization
{
    /// <summary>
    /// Deserializer for deserializing <see cref="NnLayer"/> objects.
    /// </summary>
    public class NnDeserializer
    {
        /// <summary>
        /// Deserializes data from the specified file to a <see cref="NeuralNetwork"/>.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public NeuralNetwork Deserialize(string filename)
        {
            var deserializer = new NnDeserializer();

            IEnumerable<NnLayer> layers = deserializer.DeserializeLayers(filename);

            var neuralNetwork = new NeuralNetwork(layers, 1.0);

            return neuralNetwork;
        }

        /// <summary>
        /// Deserializes data from the specified file to a collection of <see cref="NnLayer"/> objects.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>An collection of neural network layers.</returns>
        public IEnumerable<NnLayer> DeserializeLayers(string filename)
        {
            using (var deserializer = new NnBinaryDeserializer(filename, FileMode.Open))
            {
                IEnumerable<NnSerializationContext> contexts = deserializer.Deserialize();

                IEnumerable<NnLayer> result = contexts.Select(DeserializeContext);

                return result;
            }
        }

        /// <summary>
        /// Deserializes the specified <see cref="NnSerializationContext"/> to a <see cref="NnLayer"/> object.
        /// </summary>
        /// <param name="serializationContext">The serialization context.</param>
        /// <returns>The deserialized neural network layer.</returns>
        public NnLayer DeserializeContext(NnSerializationContext serializationContext)
        {
            NnSerializationContextInfo contextInfo = serializationContext.SerializationContextInfo;

            double[] data = serializationContext.FileData;

            var activationFunction = (IActivationFunction) FunctionFactory.Instance.GetInstance(contextInfo.ActivationFunctionName);
            
            var layer = new NnLayer(contextInfo.WeightMatrixRowCount, contextInfo.WeightMatrixColCount, contextInfo.BiasLength, data, activationFunction);

            return layer;
        }
    }
}
