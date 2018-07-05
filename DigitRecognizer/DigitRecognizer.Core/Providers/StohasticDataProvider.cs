using DigitRecognizer.Core.Data;
using DigitRecognizer.Core.InputOutput;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class StohasticDataProvider : IDataProvider<MnistImage>
    {
        /// <summary>
        /// 
        /// </summary>
        protected const int StohasticBatchSize = 1;

        /// <summary>
        /// Gets the memory stream reader for labels.
        /// </summary>
        private readonly LabelProvider _labelProvider;

        /// <summary>
        /// Gets the memory stream reader for images.
        /// </summary>
        private readonly PixelProvider _pixelProvider;

        /// <summary>
        /// Gets the batch size.
        /// </summary>
        private readonly int _batchSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelFilename"></param>
        /// <param name="imageFilename"></param>
        /// <param name="batchSize"></param>
        protected StohasticDataProvider(string labelFilename, string imageFilename, int batchSize)
        {
            Contracts.StringNotNullOrEmpty(labelFilename, nameof(labelFilename));
            Contracts.StringNotNullOrEmpty(imageFilename, nameof(imageFilename));
            Contracts.FileExists(labelFilename, nameof(labelFilename));
            Contracts.FileExists(imageFilename, nameof(imageFilename));
            Contracts.ValueGreaterThanZero(batchSize, nameof(batchSize));
            Contracts.ValuesMatch(_batchSize, StohasticBatchSize, nameof(_batchSize));

            _labelProvider = new LabelProvider(labelFilename, batchSize);
            _pixelProvider = new PixelProvider(labelFilename, batchSize);

            _batchSize = batchSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MnistImage GetData()
        {
            var label = _labelProvider.Read();
            var pixels = _pixelProvider.Read();

            var result = new MnistImage(label, pixels);

            return result;
        }
    }
}
