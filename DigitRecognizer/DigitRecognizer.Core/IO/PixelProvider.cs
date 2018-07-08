using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// Provides methods for reading pixels from a <see cref="MemoryStream"/>.
    /// </summary>
    public class PixelProvider : MemoryStreamReader, IPixelProvider<double>
    {
        /// <summary>
        /// The total amount of bytes an image contains.
        /// </summary>
        private const int ImageLengthInBytes = 784;

        /// <summary>
        /// A "magic number" of bytes that needs to be skipped at the beginning of the stream. 
        /// </summary>
        private const int InitialOffset = 16;

        /// <summary>
        /// The size of the batch, for the batch methods.
        /// </summary>
        private readonly int _batchSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelProvider"/> class.
        /// </summary>
        /// <param name="filePath">The file path used for instantiating an internal stream object.</param>
        /// <param name="batchSize">The size of the batch.</param>
        public PixelProvider(string filePath, int batchSize) : base(filePath, InitialOffset)
        {
            Contracts.ValueGreaterThanZero(batchSize, nameof(batchSize));

            _batchSize = batchSize;
        }

        /// <summary>
        /// Reads an array of pixels from the internal <see cref="MemoryStream"/> object.
        /// </summary>
        /// <returns>An array of doubles.</returns>
        public double[] Read()
        {
            return Read(DoubleExtensions.FromByte);
        }

        /// <summary>
        /// Reads an array of pixels from the internal <see cref="MemoryStream"/> object. The values are normalized.
        /// </summary>
        /// <returns>An array of doubles.</returns>
        public double[] ReadNormalized()
        {
            return Read(DoubleExtensions.FromByteNormalized);
        }

        /// <summary>
        /// Reads an array of pixels from the internal <see cref="MemoryStream"/> object. The values are offset to a certain range.
        /// </summary>
        /// <returns>An array of doubles.</returns>
        public double[] ReadOffset()
        {
            return Read(DoubleExtensions.FromByteOffset);
        }

        /// <summary>
        /// Reads an matrix of pixels from the internal <see cref="MemoryStream"/> object.
        /// </summary>
        /// <returns>An matrix of doubles.</returns>
        public double[][] ReadBatch()
        {
            return ReadBatch(DoubleExtensions.FromByte);
        }

        /// <summary>
        /// Reads an matrix of pixels from the internal <see cref="MemoryStream"/> object. The values are normalized.
        /// </summary>
        /// <returns>An matrix of doubles.</returns>
        public double[][] ReadBatchNormalized()
        {
            return ReadBatch(DoubleExtensions.FromByteNormalized);
        }

        /// <summary>
        /// Reads an matrix of pixels from the internal <see cref="MemoryStream"/> object.  The values are offset to a certain range.
        /// </summary>
        /// <returns>An matrix of doubles.</returns>
        public double[][] ReadBatchOffset()
        {
            return ReadBatch(DoubleExtensions.FromByteOffset);
        }

        /// <summary>
        /// Reads an array of pixels from the internal <see cref="MemoryStream"/>  object, 
        /// applying the specified conversion function to the data.
        /// </summary>
        /// <returns>An array of doubles.</returns>
        private double[] Read(Func<byte, double> conversionFunc)
        {
            return Read(ImageLengthInBytes).Select(conversionFunc).ToArray();
        }

        /// <summary>
        /// Reads an matrix of pixels from the internal <see cref="MemoryStream"/>  object, 
        /// applying the specified conversion function to the data.
        /// </summary>
        /// <returns>An matrix of doubles.</returns>
        private double[][] ReadBatch(Func<byte, double> conversionFunc)
        {
            var rawBytes = Read(_batchSize, ImageLengthInBytes);

            var result = new List<double[]>();

            for (var i = 0; i < rawBytes.Length; i++)
            {
                result.Add(rawBytes[i].Select(conversionFunc).ToArray());
            }

            return result.ToArray();
        }
    }
}
