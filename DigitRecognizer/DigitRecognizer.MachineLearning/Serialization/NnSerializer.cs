using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.IO;
using DigitRecognizer.MachineLearning.Infrastructure;

namespace DigitRecognizer.MachineLearning.Serialization
{
    public class NnSerializer : INnSerializer<NnLayer>
    {
        public void Serialize(string filename, IEnumerable<NnLayer> collection)
        {
            using (var serializer = new NnBinarySerializer(filename, FileMode.Create))
            {
                List<NnSerializationContext> files = collection.Select(layer => layer.Serialize()).ToList();

                serializer.Serialize(files);
            }
        }
    }
}
