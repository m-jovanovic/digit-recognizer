using System.Collections.Generic;

namespace DigitRecognizer.Core.DataStructures
{
    /// <summary>
    /// Represents a generic doubly linked list of <see cref="IDoublyLinkedListNode{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDoublyLinkedList<T> where T : IDoublyLinkedListNode<T>
    {
        int Count { get; }
        IDoublyLinkedListNode<T> First { get; }
        IDoublyLinkedListNode<T> Last { get; }

        void Add(IDoublyLinkedListNode<T> node);
        List<T> ToList();
    }
}
