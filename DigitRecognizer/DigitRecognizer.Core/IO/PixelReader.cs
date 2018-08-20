namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// Provides methods for reading pixels from a <see cref="System.IO.MemoryStream"/>.
    /// </summary>
    public class PixelReader : MemoryStreamReader, IPixelReader
    {
        /// <summary>
        /// A "magic number" of bytes that needs to be skipped at the beginning of the stream. 
        /// </summary>
        public const int InitialOffset = 16;

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelReader"/> class.
        /// </summary>
        /// <param name="filename">The file path used for instantiating an internal stream object.</param>
        public PixelReader(string filename) : base(filename, InitialOffset)
        {
        }

        /// <summary>
        /// Reads the specified ammount of pixels from the file.
        /// </summary>
        /// <param name="count">The number of pixels to read.</param>
        /// <returns>Am arrau of doubles.</returns>
        public double[] ReadPixels(int count)
        {
            byte[] bytes = Read(count);

            double[] result = ConvertByteArrayToDoubleArray(bytes, count);

            return result;
        }

        /// <summary>
        /// Reads the specified ammount of blocks of pixels from the file, each of the specified block size.
        /// </summary>
        /// <param name="count">The number of blocks to read.</param>
        /// <param name="blockSize">The size of each block.</param>
        /// <returns>A jagged array of doubles.</returns>
        public double[][] ReadPixels(int count, int blockSize)
        {
            byte[][] bytes = Read(count, blockSize);

            var result = new double[count][];

            for (var i = 0; i < count; i++)
            {
                result[i] = ConvertByteArrayToDoubleArray(bytes[i], blockSize);
            }

            return result;
        }

        /// <summary>
        /// Converts an array of bytes to an array of doubles.
        /// </summary>
        /// <param name="bytes">The array of bytes.</param>
        /// <param name="length">The length of the array.</param>
        /// <returns></returns>
        private static double[] ConvertByteArrayToDoubleArray(byte[] bytes, int length)
        {
            var result = new double[length];

            for (var i = 0; i < length; i++)
            {
                result[i] = bytes[i];
            }

            return result;
        }
    }
}
