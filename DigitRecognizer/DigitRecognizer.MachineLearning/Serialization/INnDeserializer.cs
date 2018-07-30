using System.Collections.Generic;
using DigitRecognizer.Core.IO;

namespace DigitRecognizer.MachineLearning.Serialization
{
    /// <summary>
    /// Interface for a class that will act as a deserializer for classes that implement <see cref="INnSerializable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the deserializer.</typeparam>
    public interface INnDeserializer<out T> where T: INnSerializable
    {
        /// <summary>
        /// Deserializes data from the specified file to a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>An <see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> Deseralize(string filename);

        /// <summary>
        /// Deserializes the specified <see cref="NnSerializationContext"/> to the type of the deserializer.
        /// </summary>
        /// <param name="serializationContext">The serialization context.</param>
        /// <returns>The deserialized object.</returns>
        T Deserialize(NnSerializationContext serializationContext);
    }
}
