using System.Collections.Generic;

namespace DigitRecognizer.Core.IO
{
    public interface INnBinarySerializer
    {
        void Serialize(NnFile file);
        void Serialize(IEnumerable<NnFile> files);
    }
}
