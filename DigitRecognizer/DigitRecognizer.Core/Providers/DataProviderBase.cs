using DigitRecognizer.Core.IO;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Providers
{
    public abstract class DataProviderBase<T> : IDataProvider<T>
    {
        /// <summary>
        /// The size of a single MNIST image in pixels.
        /// </summary>
        protected const int ImageSizeInPixels = 784;

        /// <summary>
        /// The <see cref="ILabelReader"/> instance, used for fetching labels.
        /// </summary>
        protected readonly ILabelReader LabelReader;

        /// <summary>
        /// The <see cref="IPixelReader"/> instance, used for fetching pixels.
        /// </summary>
        protected readonly IPixelReader PixelReader;

        /// <summary>
        /// The batch size for the data provider.
        /// </summary>
        protected readonly int BatchSize;

        /// <summary>
        /// Initialzies a new instance of the <see cref="DataProviderBase{T}"/> class.
        /// </summary>
        /// <param name="labelFilename">The path to the file containing the labels.</param>
        /// <param name="imageFilename">The path to the file containing the images.</param>
        /// <param name="batchSize">The size of the batch to read.</param>
        protected DataProviderBase(string labelFilename, string imageFilename, int batchSize)
        {
            Contracts.StringNotNullOrEmpty(labelFilename, nameof(labelFilename));
            Contracts.FileExists(labelFilename, nameof(labelFilename));

            Contracts.FileExists(imageFilename, nameof(imageFilename));
            Contracts.StringNotNullOrEmpty(imageFilename, nameof(imageFilename));

            Contracts.ValueGreaterThanZero(batchSize, nameof(batchSize));

            LabelReader = new LabelReader(labelFilename);
            PixelReader = new PixelReader(imageFilename);
            BatchSize = batchSize;
        }

        /// <summary>
        /// Gets the data from a file.
        /// </summary>
        /// <returns></returns>
        public virtual T GetData()
        {
            return default(T);
        }
    }
}
