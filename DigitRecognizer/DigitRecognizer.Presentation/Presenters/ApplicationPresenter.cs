using DigitRecognizer.Presentation.Services;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Presenters
{
    public class ApplicationPresenter
    {
        private readonly BenchmarkPresenter _benchmarkPresenter;
        private readonly DrawingPresenter _drawingPresenter;

        public ApplicationPresenter(IMainFormView mainFormView, 
            IMessageService messageService, 
            ILoggingService loggingService)
        {
            _benchmarkPresenter = new BenchmarkPresenter(mainFormView.BenchmarkView, messageService, loggingService);
            _drawingPresenter = new DrawingPresenter(mainFormView.DrawingView, messageService, loggingService);
        }
    }
}
