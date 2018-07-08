using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.IO;
using DigitRecognizer.MachineLearning.Data;
using DigitRecognizer.MachineLearning.Interfaces.InputOutput;

namespace DigitRecognizer.MachineLearning.Utilities
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
                var files = deserializer.DeserializeMany();

                var result = files.Select(Deserialize).ToList();

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public NnLayer Deserialize(NnFile file)
        {
            var fileInfo = file.FileInfo;
            var data = file.FileData;

            var layer = new NnLayer(fileInfo.WeightMatrixHeight,fileInfo.WeightMatrixWidth, fileInfo.BiasLength, data);

            return layer;
        }
    }
}
