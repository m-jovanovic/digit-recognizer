using DigitRecognizer.Core.IO;

namespace DigitRecognizer.MachineLearning.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public interface INnSerializable
    {
        NnSerializationContext Serialize();
    }
}
