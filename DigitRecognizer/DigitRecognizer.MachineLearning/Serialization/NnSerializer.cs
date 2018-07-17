using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.IO;
using DigitRecognizer.MachineLearning.Data;
using DigitRecognizer.MachineLearning.Interfaces.InputOutput;

namespace DigitRecognizer.MachineLearning.Utilities
{
    public class NnSerializer : INnSerializer<NnLayer>
    {
        public void Serialize(string filename, IEnumerable<NnLayer> collection)
        {
            using (var serializer = new NnBinarySerializer(filename, FileMode.Create))
            {
                var files = collection.Select(layer => layer.Serialize()).ToList();

                serializer.Serialize(files);
            }
        }
    }
}
