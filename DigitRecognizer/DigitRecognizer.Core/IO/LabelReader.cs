namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// Provides methods for reading labels from a <see cref="System.IO.MemoryStream"/>.
    /// </summary>
    public class LabelReader : MemoryStreamReader, ILabelReader
    {
        /// <summary>
        /// A "magic number" of bytes that needs to be skipped at the beginning of the stream. 
        /// </summary>
        public const int InitialOffset = 8;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelReader"/> class.
        /// </summary>
        /// <param name="filename">The file path used for instantiating an internal stream object.</param>
        public LabelReader(string filename) : base(filename, InitialOffset)
        {
        }

        /// <summary>
        /// Reads a single label from the file.
        /// </summary>
        /// <returns>A label.</returns>
        public int ReadLabel()
        {
            int result = Read(1)[0];

            return result;
        }

        /// <summary>
        /// Reads the specified ammount of labels from the file.
        /// </summary>
        /// <param name="count">The number of labels to read.</param>
        /// <returns>An array of labels.</returns>
        public int[] ReadLabels(int count)
        {
            byte[] bytes = Read(count);

            var result = new int[count];

            for (var i = 0; i < count; i++)
            {
                result[i] = bytes[i];
            }

            return result;
        }
    }
}
