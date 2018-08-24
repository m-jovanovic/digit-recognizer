using DigitRecognizer.Presentation.Services;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation.Presenters
{
    public class ApplicationPresenter
    {
        private readonly BenchmarkPresenter _benchmarkPresenter;

        public ApplicationPresenter(IMainFormView mainFormView, 
            IMessageService messageService, 
            ILoggingService loggingService)
        {
            _benchmarkPresenter = new BenchmarkPresenter(mainFormView.BenchmarkView, messageService, loggingService);
        }
    }
}
