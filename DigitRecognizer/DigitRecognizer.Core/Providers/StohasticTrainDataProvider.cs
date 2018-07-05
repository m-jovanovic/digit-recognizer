namespace DigitRecognizer.Core.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class StohasticTrainDataProvider : StohasticDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelFilename"></param>
        /// <param name="imageFilename"></param>
        public StohasticTrainDataProvider(string labelFilename, string imageFilename) : base(labelFilename, imageFilename, StohasticBatchSize)
        {
        }
    }
}
