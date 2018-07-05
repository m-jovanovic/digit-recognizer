using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.InputOutput;
using DigitRecognizer.MachineLearning.Interfaces;
using DigitRecognizer.MachineLearning.Interfaces.InputOutput;
using DigitRecognizer.MachineLearning.Interfaces.ML;

namespace DigitRecognizer.MachineLearning.Utilities
{
    public class NnSerializer : INnSerializer<INnLayer>
    {
        public void Serialize(string filename, IEnumerable<INnLayer> collection)
        {
            using (var serializer = new NnBinarySerializer(filename, FileMode.Create))
            {
                var files = collection.Select(layer => layer.Serialize()).ToList();

                serializer.Serialize(files);
            }
        }
    }
}
