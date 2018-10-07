namespace DigitRecognizer.Presentation.Views.Interfaces
{
    /// <summary>
    /// Interface for the main form view.
    /// </summary>
    public interface IMainFormView : IView
    {
        /// <summary>
        /// Gets the benchmark view.
        /// </summary>
        IBenchmarkView BenchmarkView { get; }

        /// <summary>
        /// Gets the drawing view.
        /// </summary>
        IDrawingView DrawingView { get; }
        
        /// <summary>
        /// Gets the upload image view.
        /// </summary>
        IUploadImageView UploadImageView { get; }

        /// <summary>
        /// Gets the sliding window view.
        /// </summary>
        ISlidingWindowView SlidingWindowView { get; }
    }
}
