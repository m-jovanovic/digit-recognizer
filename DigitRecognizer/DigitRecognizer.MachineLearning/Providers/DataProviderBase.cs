using DigitRecognizer.Core.IO;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.MachineLearning.Providers
{
    /// <summary>
    /// Represents a base class for a data provider.
    /// </summary>
    /// <typeparam name="T">The type of data the provider returns.</typeparam>
    public abstract class DataProviderBase<T> : IDataProvider<T>
    {
        private readonly string _labelFilename;
        private readonly string _imageFilename;
        private readonly int _batchSize;

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
        /// Gets the batch size of the data provider.
        /// </summary>
        public int BatchSize => _batchSize;

        /// <summary>
        /// Gets the name of the file containing labels.
        /// </summary>
        public string LabelFilename => _labelFilename;

        /// <summary>
        /// Gets the name of the file containing images.
        /// </summary>
        public string ImageFilename => _imageFilename;

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
            _labelFilename = labelFilename;
            _imageFilename = imageFilename;
            _batchSize = batchSize;
        }

        /// <summary>
        /// Gets the data from the file system.
        /// </summary>
        /// <returns></returns>
        public virtual T GetData()
        {
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual object LoadData()
        {
            return GetData();
        }
    }
}
