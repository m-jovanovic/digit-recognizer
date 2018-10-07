using System;
using System.Drawing;
using DigitRecognizer.Core.Data;

namespace DigitRecognizer.Presentation.Views.Interfaces
{
    public interface ISlidingWindowView : IView
    {
        event EventHandler ClassifyDrawing;
        event EventHandler ClearDrawing;

        void DrawBoundingBox(BoundingBox boundingBox, int prediction, double predictionAccuracy);
        void Clear();

        Image Drawing { get; }
    }
}
