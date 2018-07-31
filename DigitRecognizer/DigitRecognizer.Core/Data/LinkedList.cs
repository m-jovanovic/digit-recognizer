using System.Collections.Generic;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Data
{
    /// <summary>
    /// Doubly linked list of a generic type. Supports adding nodes to the collection and converting to a list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedList<T>
    {
        /// <summary>
        /// The head of the linked list.
        /// </summary>
        private LinkedListNode<T> _head;
        /// <summary>
        /// The last element of the linked list.
        /// </summary>
        private LinkedListNode<T> _last;

        private int _count;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedList{T}"/> class that is empty.
        /// </summary>
        public LinkedList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedList{T}"/> class with the specicifed node as the head.
        /// </summary>
        /// <param name="item"></param>
        public LinkedList(T item)
        {
            var node = new LinkedListNode<T>(item);

            AddFirst(node);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedList{T}"/> class with the specified node as the head.
        /// </summary>
        /// <param name="node">The node to add to the head of the list.</param>
        public LinkedList(LinkedListNode<T> node)
        {
            Contracts.ValueNotNull(node, nameof(node));
            Contracts.ValueIsNull(node.Next, nameof(node.Next));
            Contracts.ValueIsNull(node.Previous, nameof(node.Previous));

            AddFirst(node);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedList{T}"/> class with the specified collection of items.
        /// </summary>
        /// <param name="collection">The collection to add to the list.</param>
        public LinkedList(IEnumerable<T> collection)
        {
            Contracts.ValueNotNull(collection, nameof(collection));

            foreach (var item in collection)
            {
                AddLast(new LinkedListNode<T>(item));
            }
        }

        /// <summary>
        /// Adds the specified item to the last spot in the list.
        /// </summary>
        /// <param name="item">The item to add to the head of the list.</param>
        public void AddLast(T item)
        {
            var node = new LinkedListNode<T>(item);

            AddLast(node);
        }

        /// <summary>
        /// Adds the specified <see cref="LinkedListNode{T}"/> to the last spot in the list.
        /// </summary>
        /// <param name="node">The node to add to the head of the list.</param>
        public void AddLast(LinkedListNode<T> node)
        {
            node.Depth = _count;
            
            if (_head == null)
            {
                AddFirst(node);
                return;
            }

            node.Next = null;
            node.Previous = _last;
            _last.Next = node;
            _last = node;
            _count++;
        }

        /// <summary>
        /// Adds the specified <see cref="LinkedListNode{T}"/> to the head of the list.
        /// </summary>
        /// <param name="node">The node to add to the head of the list.</param>
        private void AddFirst(LinkedListNode<T> node)
        {
            _head = node;
            _head.Next = null;
            _head.Previous = null;
            _last = _head;
            _count++;
        }

        /// <summary>
        /// Converts the <see cref="LinkedList{T}"/> to a <see cref="List{T}"/>.
        /// </summary>
        /// <returns>A <see cref="List{T}"/>.</returns>
        public List<T> ToList()
        {
            var result = new List<T>();

            LinkedListNode<T> current = _head;
            while (current != null)
            {
                result.Add(current.Value);
                current = current.Next;
            }

            return result;
        }
        
        /// <summary>
        /// Gets the count property. This field is readonly.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Gets the first node in the linked list.
        /// </summary>
        public LinkedListNode<T> First => _head;

        /// <summary>
        /// Gets the last node in the linked list.
        /// </summary>
        public LinkedListNode<T> Last => _head == null ? null : _last;
    }
}
