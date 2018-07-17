using DigitRecognizer.Core.Data;

namespace DigitRecognizer.MachineLearning.Providers
{
    /// <summary>
    /// A data provider, that only retrieves a single <see cref="MnistImage"/> per request.
    /// </summary>
    public class StohasticDataProvider : DataProviderBase<MnistImage>
    {
        private const int StohasticBatchSize = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="StohasticDataProvider"/> class.
        /// </summary>
        /// <param name="labelFilename">The path to the file containing the labels.</param>
        /// <param name="imageFilename">The path to the file containing the images.</param>
        public StohasticDataProvider(string labelFilename, string imageFilename)
            : base(labelFilename, imageFilename, StohasticBatchSize)
        {
        }

        /// <summary>
        /// Gets the data from the file system.
        /// </summary>
        /// <returns></returns>
        public override MnistImage GetData()
        {
            int label = LabelReader.ReadLabel();
            double[] pixels = PixelReader.ReadPixels(ImageSizeInPixels);

            var result = new MnistImage(label, pixels);

            return result;
        }
    }
}
