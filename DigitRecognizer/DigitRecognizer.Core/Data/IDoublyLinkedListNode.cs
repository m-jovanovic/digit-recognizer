namespace DigitRecognizer.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDoublyLinkedListNode<T>
    {
        IDoublyLinkedListNode<T> Next { get; set; }
        IDoublyLinkedListNode<T> Previous { get; set; }
        T Value { get; }
    }
}
