using System.Collections.Generic;

namespace DigitRecognizer.Core.IO
{
    public interface INnBinaryDeserializer
    {
        NnFile Deserialize();
        IEnumerable<NnFile> DeserializeMany();
    }
}
