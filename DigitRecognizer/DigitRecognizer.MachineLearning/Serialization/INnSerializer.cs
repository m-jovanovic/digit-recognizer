using System.Collections.Generic;

namespace DigitRecognizer.MachineLearning.Interfaces.InputOutput
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
