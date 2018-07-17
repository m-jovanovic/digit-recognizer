using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Data
{
    /// <summary>
    /// Doubly linked list node of a generic type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedListNode<T>
    {
        private LinkedListNode<T> _previous;
        private LinkedListNode<T> _next;
        private readonly T _item;
        private int _depth;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedListNode{T}"/> class.
        /// </summary>
        /// <param name="item"></param>
        public LinkedListNode(T item)
        {
            _item = item;
        }

        /// <summary>
        /// Gets or sets the previous property.
        /// </summary>
        public LinkedListNode<T> Previous
        {
            get => _previous;
            set => _previous = value;
        }

        /// <summary>
        /// Gets or sets the next property.
        /// </summary>
        public LinkedListNode<T> Next
        {
            get => _next;
            set => _next = value;
        }

        /// <summary>
        /// Gets the value of the node. This field is readonly.
        /// </summary>
        public T Value => _item;

        /// <summary>
        /// Gets or sets the depth of the node.
        /// </summary>
        public int Depth
        {
            get => _depth;
            set
            {
                Contracts.ValueGreaterThanZero(value, nameof(value));

                _depth = value;
            }
        }
    }
}
