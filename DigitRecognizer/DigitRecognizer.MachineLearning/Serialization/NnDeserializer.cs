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
                var files = deserializer.Deserialize();

                var result = files.Select(Deserialize).ToList();

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
            var fileInfo = serializationContext.SerializationContextInfo;
            var data = serializationContext.FileData;

            var layer = new NnLayer(fileInfo.WeightMatrixColCount,fileInfo.WeightMatrixRowCount, fileInfo.BiasLength, data);

            return layer;
        }
    }
}
