namespace DigitRecognizer.Core.InputOutput
{
    /// <summary>
    /// Contains context information about a neural netowrk file.
    /// </summary>
    public class NnFileInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mWidth"></param>
        /// <param name="mHeight"></param>
        /// <param name="bLength"></param>
        public NnFileInfo(int mWidth, int mHeight, int bLength)
        {
            WeightMatrixWidth = mWidth;
            WeightMatrixHeight = mHeight;
            BiasLength = bLength;
        }

        /// <summary>
        /// Gets or sets the wieght matrix width.
        /// </summary>
        public int WeightMatrixWidth { get; set; }

        /// <summary>
        /// Gets or sets the weight matrix height.
        /// </summary>
        public int WeightMatrixHeight { get; set; }

        /// <summary>
        /// Gets or sets the biase length.
        /// </summary>
        public int BiasLength { get; set; }

        /// <summary>
        /// Gets the length of the data array.
        /// </summary>
        public int DataLength => WeightMatrixWidth * WeightMatrixHeight + BiasLength;

        /// <summary>
        /// Gets the size of the data array in bytes.
        /// </summary>
        public int DataSizeInBytes => DataLength * sizeof(double);
    }
}
