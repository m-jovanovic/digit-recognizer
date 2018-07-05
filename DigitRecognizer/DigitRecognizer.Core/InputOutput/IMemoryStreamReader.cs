namespace DigitRecognizer.Core.InputOutput
{
    public interface IMemoryStreamReader<T>
    {
        T[] Read(int count);
        T[][] Read(int count, int length);
        void Reset();
    }
}
