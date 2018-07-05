using System.Collections.Generic;
using System.Linq;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Data
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T> where T : IDoublyLinkedListNode<T>
    {
        private IDoublyLinkedListNode<T> _head;
        private int _count;
        private IDoublyLinkedListNode<T> _last;
        
        public int Count => _count;

        public IDoublyLinkedListNode<T> First => _head;

        public IDoublyLinkedListNode<T> Last => _head?.Next == null ? _head : _last;

        public DoublyLinkedList()
        {
            _head = null;
            _count = 0;
        }

        public DoublyLinkedList(IDoublyLinkedListNode<T> root) : this()
        {
            Contracts.ValueNotNull(root, nameof(root));

            AddFirst(root);
        }

        public DoublyLinkedList(IEnumerable<IDoublyLinkedListNode<T>> collection)
        {
            Contracts.ValueNotNull(collection, nameof(collection));

            var nodes = collection.ToList();
            _count = nodes.Count;

            foreach (var node in nodes)
            {
                Add(node);
            }
        }

        public void Add(IDoublyLinkedListNode<T> node)
        {
            Contracts.ValueNotNull(node, nameof(node));

            _count++;

            if (_head == null)
            {
                AddFirst(node);
                return;
            }
            
            node.Next = null;
            node.Previous = _last;

            _last.Next = node;
            _last = node;
        }

        private void AddFirst(IDoublyLinkedListNode<T> node)
        {
            _head = node;
            _head.Next = null;
            _head.Previous = null;
            _last = _head;
        }

        public List<T> ToList()
        {
            var result = new List<T>();

            var currentNode = _head;

            while (currentNode.Next != null)
            {
                result.Add(currentNode.Value);
                currentNode = currentNode.Next;
            }

            return result;
        }
    }
}
