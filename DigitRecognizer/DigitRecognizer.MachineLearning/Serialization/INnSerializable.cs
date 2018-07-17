using DigitRecognizer.Core.IO;

namespace DigitRecognizer.MachineLearning.Interfaces.InputOutput
{
    /// <summary>
    /// 
    /// </summary>
    public interface INnSerializable
    {
        NnSerializationContext Serialize();
    }
}
