using DigitRecognizer.Presentation.Services;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Presenters
{
    public class ApplicationPresenter
    {
        private readonly BenchmarkPresenter _benchmarkPresenter;
        private readonly DrawingPresenter _drawingPresenter;
        private readonly UploadImagePresenter _uploadImagePresenter;
        private readonly SlidingWindowPresenter _slidingWindowPresenter;

        public ApplicationPresenter(IMainFormView mainFormView, 
            IMessageService messageService, 
            ILoggingService loggingService)
        {
            loggingService.Log("Initializing presenters");

            _benchmarkPresenter = new BenchmarkPresenter(mainFormView.BenchmarkView, messageService, loggingService);
            _drawingPresenter = new DrawingPresenter(mainFormView.DrawingView, messageService, loggingService);
            _uploadImagePresenter = new UploadImagePresenter(mainFormView.UploadImageView, messageService, loggingService);
            _slidingWindowPresenter = new SlidingWindowPresenter(mainFormView.SlidingWindowView, messageService, loggingService);
        }
    }
}
