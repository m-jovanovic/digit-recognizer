using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// A class that can efficiently read and parse neural network information from a file.
    /// </summary>
    public class NnBinaryDeserializer : NnSerializableBase
    {
        /// <summary>
        /// The <see cref="BinaryReader"/> instance used for serialization.
        /// </summary>
        private readonly BinaryReader _reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="NnBinaryDeserializer"/> class.
        /// </summary>
        /// <param name="filename">The name of the file to write to.</param>
        /// <param name="fileMode">The file mode</param>
        public NnBinaryDeserializer(string filename, FileMode fileMode) : base(filename, fileMode)
        {
            _reader = new BinaryReader(FileStream);
        }

        /// <summary>
        /// Deserializes an <see cref="IEnumerable{T}"/> of type <see cref="NnSerializationContext"/> from a file.
        /// </summary>
        /// <returns>A list of serialization contexts.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public IEnumerable<NnSerializationContext> Deserialize()
        {
            int count = DeserializeContextCount();
            Contracts.ValueGreaterThanZero(count, nameof(count));

            var contexts = new List<NnSerializationContext>();
            var contextInfoDictionary = new Dictionary<int, NnSerializationContextInfo>();

            for (var i = 0; i < count; i++)
            {
                NnSerializationContextInfo contextInfo = DeserializeContextInfo();

                contextInfoDictionary.Add(i, contextInfo);
            }

            for (var i = 0; i < count; i++)
            {
                if (!contextInfoDictionary.TryGetValue(i, out NnSerializationContextInfo contextInfo))
                {
                    throw new NullReferenceException("No context info with was found for the given key.");
                }

                double[] contextData = DeserializeContextData(contextInfo.DataSizeInBytes);

                var context = new NnSerializationContext(contextData, contextInfo);

                contexts.Add(context);
            }

            return contexts;
        }

        /// <summary>
        /// Deserializes a <see cref="NnSerializationContext"/> from a file.
        /// </summary>
        /// <returns>A serialization context.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public NnSerializationContext DeserializeSingle()
        {
            int count = DeserializeContextCount();
            Contracts.ValueGreaterThanZero(count, nameof(count));

            if (count > 1)
            {
                throw new NotSupportedException("Can not deserialize a single context, from a file that stores multiple contexts.");
            }

            NnSerializationContextInfo contextInfo = DeserializeContextInfo();

            double[] contextData = DeserializeContextData(contextInfo.DataSizeInBytes);

            var context = new NnSerializationContext(contextData, contextInfo);

            return context;
        }

        /// <summary>
        /// Reads the context count from a file.
        /// </summary>
        /// <returns>The number of contexts in the file.</returns>
        private int DeserializeContextCount()
        {
            int count = _reader.ReadInt32();

            return count;
        }

        /// <summary>
        /// Deserialize a <see cref="NnSerializationContextInfo"/> from a file.
        /// </summary>
        /// <returns>A context info.</returns>
        private NnSerializationContextInfo DeserializeContextInfo()
        {
            int weightMatrixRowCount = _reader.ReadInt32();
            int weightMatrixColCount = _reader.ReadInt32();
            int biasLength = _reader.ReadInt32();

            int activationFunctionNameSizeInBytes = _reader.ReadInt32();
            byte[] activationFunctionNameBytes = _reader.ReadBytes(activationFunctionNameSizeInBytes);
            string activationFunctionName = Encoding.Unicode.GetString(activationFunctionNameBytes);

            var contextInfo = new NnSerializationContextInfo(weightMatrixRowCount, weightMatrixColCount, biasLength, activationFunctionName);

            return contextInfo;
        }

        /// <summary>
        /// Deserializes the data from a file.
        /// </summary>
        /// <param name="count">The number of bytes to read from the file.</param>
        /// <returns>An array of doubles.</returns>
        private double[] DeserializeContextData(int count)
        {
            byte[] byteData = _reader.ReadBytes(count);

            double[] data = byteData.ToDoubles();

            return data;
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
                _reader?.Dispose();
            }

            _disposed = true;

            base.Dispose(true);
        }
    }
}
