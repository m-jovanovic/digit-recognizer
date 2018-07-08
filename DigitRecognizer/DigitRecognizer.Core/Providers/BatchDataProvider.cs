using DigitRecognizer.Core.DataStructures;
using DigitRecognizer.Core.IO;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BatchDataProvider : IDataProvider<MnistImageBatch>
    {
        /// <summary>
        /// 
        /// </summary>
        protected const int MiniBatchSize = 100;

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
        protected BatchDataProvider(string labelFilename, string imageFilename, int batchSize)
        {
            Contracts.StringNotNullOrEmpty(labelFilename, nameof(labelFilename));
            Contracts.StringNotNullOrEmpty(imageFilename, nameof(imageFilename));
            Contracts.FileExists(labelFilename, nameof(labelFilename));
            Contracts.FileExists(imageFilename, nameof(imageFilename));
            Contracts.ValueGreaterThanZero(batchSize, nameof(batchSize));
            Contracts.ValuesMatch(_batchSize, MiniBatchSize, nameof(_batchSize));

            _labelProvider = new LabelProvider(labelFilename, batchSize);
            _pixelProvider = new PixelProvider(labelFilename, batchSize);

            _batchSize = batchSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MnistImageBatch GetData()
        {
            var label = _labelProvider.ReadBatch();
            var pixels = _pixelProvider.ReadBatch();

            var result = new MnistImageBatch(label, pixels);

            return result;
        }
    }
}
