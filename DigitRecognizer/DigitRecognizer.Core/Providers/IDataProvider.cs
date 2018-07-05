namespace DigitRecognizer.Core.Providers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProvider<out T>
    {
        T GetData();
    }
}
