using System;
using System.Drawing;

namespace DigitRecognizer.Presentation.Views.Interfaces
{
    public interface IDrawingView : IView
    {
        event EventHandler ClassifyDrawing;
        event EventHandler ClearDrawing;

        void Clear();
        void ProcessPrediction(double[] prediction);

        Image Drawing { get; }
    }
}
