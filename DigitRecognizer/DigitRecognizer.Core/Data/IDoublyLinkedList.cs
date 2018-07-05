using System.Collections.Generic;

namespace DigitRecognizer.Core.Data
{
    public interface IDoublyLinkedList<T>
    {
        int Count { get; }
        IDoublyLinkedListNode<T> First { get; }
        IDoublyLinkedListNode<T> Last { get; }

        void Add(IDoublyLinkedListNode<T> node);
        List<T> ToList();
    }
}
