using System.Collections.Generic;
using System.Linq;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.DataStructures
{
    /// <summary>
    /// Represents a generic doubly linked list of <see cref="IDoublyLinkedListNode{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoublyLinkedList<T> : IDoublyLinkedList<T> where T : IDoublyLinkedListNode<T>
    {
        private IDoublyLinkedListNode<T> _last;

        /// <summary>
        /// Gets the number of nodes in the list.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the first <see cref="IDoublyLinkedListNode{T}"/> of the list.
        /// </summary>
        public IDoublyLinkedListNode<T> First { get; private set; }

        /// <summary>
        /// Gets the last <see cref="IDoublyLinkedListNode{T}"/> of the list.
        /// </summary>
        public IDoublyLinkedListNode<T> Last => First?.Next == null ? First : _last;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoublyLinkedList{T}"/> class.
        /// </summary>
        public DoublyLinkedList()
        {
            First = null;
            Count = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoublyLinkedList{T}"/> class with the specified root node.
        /// </summary>
        /// <param name="root"></param>
        public DoublyLinkedList(IDoublyLinkedListNode<T> root) : this()
        {
            Contracts.ValueNotNull(root, nameof(root));

            AddToStart(root);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoublyLinkedList{T}"/> class with the specified collection of nodes.
        /// </summary>
        /// <param name="collection"></param>
        public DoublyLinkedList(IEnumerable<IDoublyLinkedListNode<T>> collection)
        {
            Contracts.ValueNotNull(collection, nameof(collection));

            var nodes = collection.ToList();
            Count = nodes.Count;

            foreach (var node in nodes)
            {
                Add(node);
            }
        }

        /// <summary>
        /// Inserts the specified <see cref="IDoublyLinkedListNode{T}"/> to the list.
        /// </summary>
        /// <param name="node"></param>
        public void Add(IDoublyLinkedListNode<T> node)
        {
            Contracts.ValueNotNull(node, nameof(node));

            Count++;

            if (First == null)
            {
                AddToStart(node);
                return;
            }
            
            node.Next = null;
            node.Previous = _last;

            _last.Next = node;
            _last = node;
        }

        /// <summary>
        /// Inserts the specified <see cref="IDoublyLinkedListNode{T}"/> to the start of the list.
        /// </summary>
        /// <param name="node"></param>
        private void AddToStart(IDoublyLinkedListNode<T> node)
        {
            First = node;
            First.Next = null;
            First.Previous = null;
            _last = First;
        }

        /// <summary>
        /// Converts this <see cref="DoublyLinkedList{T}"/> to a <see cref="List{T}"/>.
        /// </summary>
        /// <returns></returns>
        public List<T> ToList()
        {
            var result = new List<T>();

            var currentNode = First;

            while (currentNode.Next != null)
            {
                result.Add(currentNode.Value);
                currentNode = currentNode.Next;
            }

            return result;
        }
    }
}
