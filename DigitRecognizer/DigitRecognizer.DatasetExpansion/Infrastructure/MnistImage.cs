using System;

namespace DigitRecognizer.DatasetExpansion.Infrastructure
{
    /// <summary>
    /// Represents a raw MNIST image in binary format.
    /// </summary>
    public class MnistImage
    {
        private byte _label;
        private byte[] _pixels;

        /// <summary>
        /// Initializes a new instance of the <see cref="MnistImage"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="pixels">The pixels.</param>
        public MnistImage(byte label, byte[] pixels)
        {
            _label = label;
            _pixels = pixels;
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public byte Label => _label;

        /// <summary>
        /// Gets the pixels.
        /// </summary>
        public byte[] Pixels => _pixels;

        public override string ToString()
        {
            string result = string.Empty;

            for (var i = 0; i < 28; i++)
            {
                for (var j = 0; j < 28; j++)
                {
                    result += _pixels[i * 28 + j] > 10 ? "0" : " ";
                }

                result += Environment.NewLine;
            }

            return result;
        }
    }
}
