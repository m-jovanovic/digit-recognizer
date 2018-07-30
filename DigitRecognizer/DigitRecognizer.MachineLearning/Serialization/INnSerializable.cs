using DigitRecognizer.Core.IO;

namespace DigitRecognizer.MachineLearning.Serialization
{
    /// <summary>
    /// Neural network layer will implement this interface in order to support serialization to a binary file.
    /// </summary>
    public interface INnSerializable
    {
        /// <summary>
        /// Seralizes the object to a <see cref="NnSerializationContext"/>.
        /// </summary>
        /// <returns>The serialization context containing information about the object.</returns>
        NnSerializationContext Serialize();
    }
}
