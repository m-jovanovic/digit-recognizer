using DigitRecognizer.Core.Data;

namespace DigitRecognizer.MachineLearning.Providers
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
        /// Gets the data from the file system.
        /// </summary>
        /// <returns></returns>
        public override MnistImageBatch GetData()
        {
            int[] label = LabelReader.ReadLabels(BatchSize);

            double[][] pixels = PixelReader.ReadPixels(BatchSize, ImageSizeInPixels);

            pixels = NormalizePixels(pixels);

            var result = new MnistImageBatch(label, pixels);

            return result;
        }

        /// <summary>
        /// Normalizes the specified matrix of pixels to be in the rande [0,1].
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <returns>The clamped pixels.</returns>
        private double[][] NormalizePixels(double[][] pixels)
        {
            for (var i = 0; i < pixels.Length; i++)
            {
                for (var j = 0; j < pixels[0].Length; j++)
                {
                    pixels[i][j] = pixels[i][j] / 255d;
                }
            }

            return pixels;
        }

        /// <summary>
        /// Loads the data from a source file.
        /// </summary>
        /// <returns>The data object.</returns>
        public override object LoadData()
        {
            return GetData();
        }
    }
}
