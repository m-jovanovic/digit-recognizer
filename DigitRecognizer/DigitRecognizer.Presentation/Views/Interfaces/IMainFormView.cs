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
    }
}
