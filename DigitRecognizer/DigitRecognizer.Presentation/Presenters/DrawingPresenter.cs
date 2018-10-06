using System;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.IO;
using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.Presentation.Services;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Presenters
{
    public class DrawingPresenter
    {
        #region Fields

        private readonly IDrawingView _drawingView;
        private readonly IMessageService _messageService;
        private readonly ILoggingService _loggingService;

        #endregion

        #region Ctor

        public DrawingPresenter(IDrawingView drawingView, IMessageService messageService, ILoggingService loggingService)
        {
            _messageService = messageService;
            _loggingService = loggingService;
            _drawingView = drawingView;

            _drawingView.ClassifyDrawing += OnClassifyDrawing;

            _drawingView.ClearDrawing += OnClearDrawing;
        }

        #endregion

        #region Methods
        
        private void OnClassifyDrawing(object sender, EventArgs e)
        {
            var imagePreprocessor = new ImagePreprocessor();

            double[] pixels = imagePreprocessor.Preprocess(_drawingView.Drawing);

            IPredictionModel predictionModel = Global.PredictionModel;

            double[] prediction = predictionModel.Predict(pixels);

            _drawingView.ProcessPrediction(prediction);
        }

        private void OnClearDrawing(object sender, EventArgs e)
        {
            _drawingView.Clear();
        }

        #endregion
    }
}