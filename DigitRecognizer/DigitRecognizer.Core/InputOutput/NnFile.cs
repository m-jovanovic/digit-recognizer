namespace DigitRecognizer.Core.InputOutput
{
    /// <summary>
    /// Model for the neural netowrk file format.
    /// </summary>
    public class NnFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="fileInfo"></param>
        public NnFile(double[] fileData, NnFileInfo fileInfo)
        {
            FileData = fileData;
            FileInfo = fileInfo;
        }

        /// <summary>
        /// Gets or sets the <see cref="NnFileInfo"/>.
        /// </summary>
        public NnFileInfo FileInfo { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// <para>
        /// The data is a combined array of all weights and biases.
        /// </para>
        /// </summary>
        public double[] FileData { get; set; }
    }
}
