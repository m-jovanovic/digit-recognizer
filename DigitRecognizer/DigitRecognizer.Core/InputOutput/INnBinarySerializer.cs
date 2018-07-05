using System.Collections.Generic;

namespace DigitRecognizer.Core.InputOutput
{
    public interface INnBinarySerializer
    {
        void Serialize(NnFile file);
        void Serialize(IEnumerable<NnFile> files);
    }
}
