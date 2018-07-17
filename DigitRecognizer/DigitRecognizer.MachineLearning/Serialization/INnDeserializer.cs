using System.Collections.Generic;
using DigitRecognizer.Core.IO;

namespace DigitRecognizer.MachineLearning.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INnDeserializer<out T> where T: INnSerializable
    {
        IEnumerable<T> Deseralize(string filename);
        T Deserialize(NnSerializationContext serializationContext);
    }
}
