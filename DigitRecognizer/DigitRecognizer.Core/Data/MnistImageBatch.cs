using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Data
{
    /// <summary>
    /// Represents a batch of MNIST images.
    /// </summary>
    public class MnistImageBatch
    {
        /// <summary>
        /// Gets the labels.
        /// </summary>
        public int[] Labels { get; }

        /// <summary>
        /// Gets the pixels.
        /// </summary>
        public double[][] Pixels { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MnistImageBatch"/> class.
        /// </summary>
        /// <param name="labels">The labels of the batch.</param>
        /// <param name="pixels">The pixels of the batch.</param>
        public MnistImageBatch(int[] labels, double[][] pixels)
        {
            Contracts.ValuesMatch(labels.Length, pixels.Length, nameof(labels.Length));

            Labels = labels;
            Pixels = pixels;
        }
    }
}
