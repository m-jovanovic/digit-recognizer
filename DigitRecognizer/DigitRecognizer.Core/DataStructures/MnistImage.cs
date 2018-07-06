namespace DigitRecognizer.Core.DataStructures
{
    /// <summary>
    /// Represents an MNIST image model.
    /// </summary>
    public class MnistImage
    {
        /// <summary>
        /// Gets the label.
        /// </summary>
        public int Label { get; }

        /// <summary>
        /// Gets the pixels.
        /// </summary>
        public double[] Pixels { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MnistImage"/> class.
        /// </summary>
        /// <param name="label">The label of the image.</param>
        /// <param name="pixels">The pixels of the image.</param>
        public MnistImage(int label, double[] pixels)
        {
            Label = label;
            Pixels = pixels;
        }
    }
}
