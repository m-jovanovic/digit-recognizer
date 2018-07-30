using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.IO;
using DigitRecognizer.MachineLearning.Infrastructure;

namespace DigitRecognizer.MachineLearning.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public class NnDeserializer : INnDeserializer<NnLayer>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IEnumerable<NnLayer> Deseralize(string filename)
        {
            using (var deserializer = new NnBinaryDeserializer(filename, FileMode.Open))
            {
                IEnumerable<NnSerializationContext> contexts = deserializer.Deserialize();

                List<NnLayer> result = contexts.Select(Deserialize).ToList();

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializationContext"></param>
        /// <returns></returns>
        public NnLayer Deserialize(NnSerializationContext serializationContext)
        {
            NnSerializationContextInfo contextInfo = serializationContext.SerializationContextInfo;
            double[] data = serializationContext.FileData;

            var layer = new NnLayer(contextInfo.WeightMatrixRowCount, contextInfo.WeightMatrixColCount, contextInfo.BiasLength, data, contextInfo.ActivationFunctionName);

            return layer;
        }
    }
}
