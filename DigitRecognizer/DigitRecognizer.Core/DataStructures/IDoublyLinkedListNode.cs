namespace DigitRecognizer.Core.DataStructures
{
    /// <summary>
    /// Represents a generic doubly linked list node.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDoublyLinkedListNode<T>
    {
        IDoublyLinkedListNode<T> Next { get; set; }
        IDoublyLinkedListNode<T> Previous { get; set; }
        T Value { get; }
    }
}
