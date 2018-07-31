namespace DigitRecognizer.Core.Data
{
    /// <summary>
    /// Represents a box with configurable dimensions.
    /// </summary>
    public struct Box
    {
        private int _top;
        private int _bottom;
        private int _left;
        private int _right;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Box"/> struct.
        /// </summary>
        /// <param name="top">The top value.</param>
        /// <param name="bottom">The bottom value.</param>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        public Box(int top, int bottom, int left, int right)
        {
            _top = top;
            _bottom = bottom;
            _left = left;
            _right = right;
        }

        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        public int Top
        {
            get => _top;
            set => _top = value;
        }
        
        /// <summary>
        /// Gets or sets the bottom.
        /// </summary>
        public int Bottom
        {
            get => _bottom;
            set => _bottom = value;
        }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public int Left
        {
            get => _left;
            set => _left = value;
        }
        
        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        public int Right
        {
            get => _right;
            set => _right = value;
        }
    }
}
