using System.Collections.Generic;

namespace DigitRecognizer.MachineLearning.Serialization
{
    /// <summary>
    /// Interface for a class that will act as a serializer for classes that implement <see cref="INnSerializable"/>.
    /// </summary>
    /// <typeparam name="T">The type of the serializer.</typeparam>
    public interface INnSerializer<in T> where T: INnSerializable 
    {
        /// <summary>
        /// Serializes the specified collection of objects to a file.
        /// </summary>
        /// <param name="filename">The filename to write to.</param>
        /// <param name="collection">The collection of objects to serialize.</param>
        void Serialize(string filename, IEnumerable<T> collection);
    }
}
