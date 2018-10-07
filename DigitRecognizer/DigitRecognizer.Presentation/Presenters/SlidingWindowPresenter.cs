using System;
using System.Drawing;
using System.Windows.Forms;
using DigitRecognizer.Core.Data;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.IO;
using DigitRecognizer.Core.Utilities;
using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.Presentation.Services;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Presenters
{
    public class SlidingWindowPresenter
    {
        #region Fields

        private readonly ISlidingWindowView _slidingWindowView;
        private readonly IMessageService _messageService;
        private readonly ILoggingService _loggingService;

        private static readonly Size[] WindowSizes =
        {
            new Size(280, 280),
            //new Size(140, 140),
            //new Size(112, 112),
            //new Size(84, 84),
            //new Size(56, 56)
        };

        #endregion

        #region Ctor

        public SlidingWindowPresenter(ISlidingWindowView slidingWindowView, IMessageService messageService, ILoggingService loggingService)
        {
            _messageService = messageService;
            _loggingService = loggingService;
            _slidingWindowView = slidingWindowView;

            _slidingWindowView.ClassifyDrawing += OnClassifyDrawing;

            _slidingWindowView.ClearDrawing += OnClearDrawing;
        }

        #endregion

        #region Methods

        private void OnClassifyDrawing(object sender, EventArgs e)
        {
            try
            {
                _loggingService.Log("Classify drawing has started");

                var imagePreprocessor = new ImagePreprocessor();

                IPredictionModel predictionModel = Global.PredictionModel;

                Image img = _slidingWindowView.Drawing;

                foreach (Size windowSize in WindowSizes)
                {
                    foreach (BoundingBox boundingBox in ImageUtilities.SlidingWindow(img, windowSize, 112))
                    {
                        try
                        {
                            double[] pixels = imagePreprocessor.Preprocess(boundingBox.Image);

                            double[] prediction = predictionModel.Predict(pixels);

                            // If classification is over 99% draw a bounding box at this location
                            int predicted = prediction.ArgMax();
                            double predictedAccuracy = prediction[prediction.ArgMax()];

                            if (predictedAccuracy >= 0.95)
                            {
                                _slidingWindowView.DrawBoundingBox(boundingBox, predicted, predictedAccuracy);
                            }
                        }
                        catch (Exception exception)
                        {
                            _loggingService.Log(exception);
                        }
                    }
                }

                _loggingService.Log("Classify drawing has completed");
            }
            catch (Exception exception)
            {
                _loggingService.Log(exception);

                _messageService.ShowMessage("An error ocurred while classyfing the drawing. Please try again.", "Classification error", icon: MessageBoxIcon.Information);
            }
        }

        private void OnClearDrawing(object sender, EventArgs e)
        {
            _slidingWindowView.Clear();
        }

        #endregion
    }
}
