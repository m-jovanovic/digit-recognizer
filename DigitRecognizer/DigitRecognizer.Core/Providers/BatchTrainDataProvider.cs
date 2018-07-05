namespace DigitRecognizer.Core.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BatchTrainDataProvider : BatchDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelFilename"></param>
        /// <param name="imageFilename"></param>
        public BatchTrainDataProvider(string labelFilename, string imageFilename) : base(labelFilename, imageFilename, MiniBatchSize)
        {
        }
    }
}
