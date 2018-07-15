using DigitRecognizer.Core.Data;

namespace DigitRecognizer.Core.Providers
{
    /// <summary>
    /// A data provider, that retrieves a <see cref="MnistImageBatch"/>. The size of the batch is configurable.
    /// </summary>
    public class BatchDataProvider : DataProviderBase<MnistImageBatch>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchDataProvider"/> class.
        /// </summary>
        /// <param name="labelFilename">The path to the file containing the labels.</param>
        /// <param name="imageFilename">The path to the file containing the images.</param>
        /// <param name="batchSize">The size of the batch to read.</param>
        public BatchDataProvider(string labelFilename, string imageFilename, int batchSize)
            : base(labelFilename, imageFilename, batchSize)
        {
        }

        /// <summary>
        /// Gets the data from the fily sistem.
        /// </summary>
        /// <returns></returns>
        public override MnistImageBatch GetData()
        {
            int[] label = LabelReader.ReadLabels(BatchSize);
            double[][] pixels = PixelReader.ReadPixels(BatchSize, ImageSizeInPixels);

            var result = new MnistImageBatch(label, pixels);

            return result;
        }
    }
}
