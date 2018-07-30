using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DigitRecognizer.Core.Extensions;

namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// A class that can efficiently write neural network information to a file.
    /// </summary>
    public class NnBinarySerializer : NnSerializableBase
    {
        /// <summary>
        /// The <see cref="BinaryWriter"/> instance used for serialization.
        /// </summary>
        private readonly BinaryWriter _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="NnBinarySerializer"/> class.
        /// </summary>
        /// <param name="filename">The name of the file to write to.</param>
        /// <param name="fileMode">The file mode</param>
        public NnBinarySerializer(string filename, FileMode fileMode) : base(filename, fileMode)
        {
            _writer = new BinaryWriter(FileStream);
        }

        /// <summary>
        /// Serializes the specified <see cref="NnSerializationContext"/> to a file.
        /// </summary>
        /// <param name="serializationContext">The serialization context.</param>
        public void Serialize(NnSerializationContext serializationContext)
        {
            SerializeContextCount(1);
            SerializeContextInfo(serializationContext.SerializationContextInfo);
            SerializeContextData(serializationContext.FileData);
        }

        /// <summary>
        /// Serializes the specified <see cref="IEnumerable{T}"/> of type <see cref="NnSerializationContext"/> to a file.
        /// </summary>
        /// <param name="collection">The collection of objects to serialize.</param>
        public void Serialize(IEnumerable<NnSerializationContext> collection)
        {
            NnSerializationContext[] contexts = collection as NnSerializationContext[] ?? collection.ToArray();
            int count = contexts.Length;

            SerializeContextCount(count);

            for (var i = 0; i < count; i++)
            {
                SerializeContextInfo(contexts[i].SerializationContextInfo);
            }

            for (var i = 0; i < count; i++)
            {
                SerializeContextData(contexts[i].FileData);
            }
        }

        /// <summary>
        /// Writes the specified number to a file.
        /// </summary>
        /// <param name="count">The number of contexts being serialized.</param>
        private void SerializeContextCount(int count)
        {
            _writer.Write(count);
        }

        /// <summary>
        /// Writes the specified <see cref="NnSerializationContextInfo"/> to a file.
        /// </summary>
        /// <param name="serializationContextInfo">The context info being serialized.</param>
        private void SerializeContextInfo(NnSerializationContextInfo serializationContextInfo)
        {
            _writer.Write(serializationContextInfo.WeightMatrixRowCount);
            _writer.Write(serializationContextInfo.WeightMatrixColCount);
            _writer.Write(serializationContextInfo.BiasLength);

            byte[] bytes = Encoding.Unicode.GetBytes(serializationContextInfo.ActivationFunctionName);
            _writer.Write(serializationContextInfo.ActivationFunctionNameSizeInBytes);
            _writer.Write(bytes);
        }

        /// <summary>
        /// Writes the specified data to a file.
        /// </summary>
        /// <param name="fileData">The data of the file</param>
        private void SerializeContextData(double[] fileData)
        {
            _writer.Write(fileData.ToBytes());
        }

        /// <summary>
        /// Indicates if the disposing operation has been completed or not.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Releases all resources used by the <see cref="NnBinarySerializer"/>.
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="NnBinarySerializer"/>.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _writer?.Dispose();
            }

            _disposed = true;

            base.Dispose(true);
        }
    }
}
