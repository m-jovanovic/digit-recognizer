using System;
using System.Drawing;

namespace DigitRecognizer.Presentation.Views.Interfaces
{
    public interface IUploadImageView : IView
    {
        event EventHandler ClassifyImage;
        event EventHandler ClearImage;

        void Clear();
        void ProcessPrediction(double[] prediction);

        Image Image { get; }
    }
}
