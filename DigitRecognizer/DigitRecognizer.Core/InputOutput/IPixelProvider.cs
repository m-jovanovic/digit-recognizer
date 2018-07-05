namespace DigitRecognizer.Core.InputOutput
{
    /// <summary>
    /// Interface that should be implemented by a pixel provider class.
    /// </summary>
    /// <typeparam name="T">The type of the data.</typeparam>
    public interface IPixelProvider<T>
    {
        T[] Read();
        T[] ReadNormalized();
        T[] ReadOffset();
        T[][] ReadBatch();
        T[][] ReadBatchNormalized();
        T[][] ReadBatchOffset();
    }
}
