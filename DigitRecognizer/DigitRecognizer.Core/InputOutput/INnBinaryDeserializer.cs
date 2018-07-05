using System.Collections.Generic;

namespace DigitRecognizer.Core.InputOutput
{
    public interface INnBinaryDeserializer
    {
        NnFile Deserialize();
        IEnumerable<NnFile> DeserializeMany();
    }
}
