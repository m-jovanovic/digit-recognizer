using System;
using System.Windows.Forms;
using DigitRecognizer.Core.IO;
using DigitRecognizer.MachineLearning.Infrastructure.Models;
using DigitRecognizer.Presentation.Services;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Presenters
{
    public class UploadImagePresenter
    {
        #region Fields

        private readonly IUploadImageView _uploadImageView;
        private readonly IMessageService _messageService;
        private readonly ILoggingService _loggingService;

        #endregion

        #region Ctor


        public UploadImagePresenter(IUploadImageView uploadImageView, IMessageService messageService, ILoggingService loggingService)
        {
            _uploadImageView = uploadImageView;
            _messageService = messageService;
            _loggingService = loggingService;

            _uploadImageView.ClassifyImage += OnClassifyImage;

            _uploadImageView.ClearImage += OnClearImage;
        }

        #endregion

        #region Methods

        private void OnClassifyImage(object sender, EventArgs e)
        {
            try
            {
                _loggingService.Log("Classify drawing has started");

                var imagePreprocessor = new ImagePreprocessor();

                double[] pixels = imagePreprocessor.Preprocess(_uploadImageView.Image);

                IPredictionModel predictionModel = Global.PredictionModel;

                double[] prediction = predictionModel.Predict(pixels);

                _uploadImageView.ProcessPrediction(prediction);

                _loggingService.Log("Classify drawing has completed");
            }
            catch (NullReferenceException exception)
            {
                _loggingService.Log(exception);

                _messageService.ShowMessage("No image was uploaded. Please upload an image and try again.", "Upload error", icon: MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                _loggingService.Log(exception);

                _messageService.ShowMessage("An error ocurred while classyfing the drawing. Please try again.", "Classification error", icon: MessageBoxIcon.Information);
            }
        }

        private void OnClearImage(object sender, EventArgs e)
        {
            _uploadImageView.Clear();
        }

        #endregion
    }
}
