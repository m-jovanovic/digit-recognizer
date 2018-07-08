namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// Interface that should be implemented by a label provider class.
    /// </summary>
    /// <typeparam name="T">The type of the data.</typeparam>
    public interface ILabelProvider<T>
    {
        T Read();
        T[] ReadBatch();
    }
}
