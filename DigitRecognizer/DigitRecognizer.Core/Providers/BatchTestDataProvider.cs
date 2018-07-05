namespace DigitRecognizer.Core.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BatchTestDataProvider : BatchDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelFilename"></param>
        /// <param name="imageFilename"></param>
        public BatchTestDataProvider(string labelFilename, string imageFilename) : base(labelFilename, imageFilename, MiniBatchSize)
        {
        }
    }
}
