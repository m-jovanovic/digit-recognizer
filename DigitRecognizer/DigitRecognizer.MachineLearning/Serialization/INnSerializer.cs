using System.Collections.Generic;

namespace DigitRecognizer.MachineLearning.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INnSerializer<in T> where T: INnSerializable 
    {
        void Serialize(string filename, IEnumerable<T> collection);
    }
}
