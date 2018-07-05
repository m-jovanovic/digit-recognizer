namespace DigitRecognizer.Core.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class StohasticTestDataProvider : StohasticDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelFilename"></param>
        /// <param name="imageFilename"></param>
        public StohasticTestDataProvider(string labelFilename, string imageFilename) : base(labelFilename, imageFilename, StohasticBatchSize)
        {
        }
    }
}
