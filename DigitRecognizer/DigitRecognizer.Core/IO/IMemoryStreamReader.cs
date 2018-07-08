namespace DigitRecognizer.Core.IO
{
    public interface IMemoryStreamReader<T>
    {
        T[] Read(int count);
        T[][] Read(int count, int length);
        void Reset();
    }
}
