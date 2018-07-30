using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.IO;
using DigitRecognizer.MachineLearning.Functions;
using DigitRecognizer.MachineLearning.Infrastructure;
using DigitRecognizer.MachineLearning.Infrastructure.Factories;

namespace DigitRecognizer.MachineLearning.Serialization
{
    /// <summary>
    /// Deserializer for deserializing <see cref="NnLayer"/> objects.
    /// </summary>
    public class NnDeserializer : INnDeserializer<NnLayer>
    {
        /// <summary>
        /// Deserializes data from the specified file to a collection of <see cref="NnLayer"/> objects.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>An collection of neural network layers.</returns>
        public IEnumerable<NnLayer> Deseralize(string filename)
        {
            using (var deserializer = new NnBinaryDeserializer(filename, FileMode.Open))
            {
                IEnumerable<NnSerializationContext> contexts = deserializer.Deserialize();

                IEnumerable<NnLayer> result = contexts.Select(Deserialize);

                return result;
            }
        }

        /// <summary>
        /// Deserializes the specified <see cref="NnSerializationContext"/> to a <see cref="NnLayer"/> object.
        /// </summary>
        /// <param name="serializationContext">The serialization context.</param>
        /// <returns>The deserialized neural network layer.</returns>
        public NnLayer Deserialize(NnSerializationContext serializationContext)
        {
            NnSerializationContextInfo contextInfo = serializationContext.SerializationContextInfo;
            double[] data = serializationContext.FileData;

            var activationFunction = (IActivationFunction) FunctionFactory.Instance.GetFunction(contextInfo.ActivationFunctionName);
            
            var layer = new NnLayer(contextInfo.WeightMatrixRowCount, contextInfo.WeightMatrixColCount, contextInfo.BiasLength, data, activationFunction);

            return layer;
        }
    }
}
