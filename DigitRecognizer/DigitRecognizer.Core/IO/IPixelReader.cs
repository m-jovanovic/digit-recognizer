namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// Interface that should be implemented by a pixel provider class.
    /// </summary>
    public interface IPixelReader
    {
        double[] ReadPixels(int count);
        double[][] ReadPixels(int count, int blockSize);
    }
}
