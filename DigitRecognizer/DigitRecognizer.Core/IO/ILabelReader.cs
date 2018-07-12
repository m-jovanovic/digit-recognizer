namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// Interface that should be implemented by a label provider class.
    /// </summary>
    public interface ILabelReader
    {
        int ReadLabel();
        int[] ReadLabels(int count);
    }
}
