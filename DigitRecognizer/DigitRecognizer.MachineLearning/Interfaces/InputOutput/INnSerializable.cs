using DigitRecognizer.Core.InputOutput;

namespace DigitRecognizer.MachineLearning.Interfaces.InputOutput
{
    /// <summary>
    /// 
    /// </summary>
    public interface INnSerializable
    {
        NnFile Serialize();
    }
}
