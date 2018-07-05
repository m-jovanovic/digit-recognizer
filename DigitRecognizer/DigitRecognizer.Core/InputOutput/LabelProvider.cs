using System.IO;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.InputOutput
{
    public class LabelProvider : MemoryStreamReader, ILabelProvider<int>
    {
        /// <summary>
        /// A "magic number" of bytes that needs to be skipped at the beginning of the stream. 
        /// </summary>
        private const int MagicNumber = 8;

        /// <summary>
        /// The size of the batch, for the batch method.
        /// </summary>
        private readonly int _batchSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelProvider"/> class.
        /// </summary>
        /// <param name="filePath">The file path used for instantiating an internal stream object.</param>
        /// <param name="batchSize">The size of the batch.</param>
        public LabelProvider(string filePath, int batchSize) : base(filePath, MagicNumber)
        {
            Contracts.ValueGreaterThanZero(batchSize, nameof(batchSize));

            _batchSize = batchSize;
        }

        /// <summary>
        /// Reads a single label from an internal <see cref="MemoryStream"/> object.
        /// </summary>
        /// <returns>A integer respresenting a label.</returns>
        public int Read()
        {
            return Read(1)[0];
        }

        /// <summary>
        /// Reads a batch of labels from an internal <see cref="MemoryStream"/> object.
        /// </summary>
        /// <returns>An array of integers.</returns>
        public int[] ReadBatch()
        {
            var rawBytes = Read(_batchSize);

            var result = new int[rawBytes.Length];

            for (var i = 0; i < rawBytes.Length; i++)
            {
                result[i] = rawBytes[i];
            }

            return result;
        }
    }
}
