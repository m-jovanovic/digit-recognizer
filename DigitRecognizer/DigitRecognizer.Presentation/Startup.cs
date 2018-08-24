using DigitRecognizer.Presentation.Infrastructure;
using DigitRecognizer.Presentation.Presenters;
using DigitRecognizer.Presentation.Services;
using DigitRecognizer.Presentation.Views.Implementations;
using DigitRecognizer.Presentation.Views.Interfaces;

namespace DigitRecognizer.Presentation
{
    public static class Startup
    {
        public static void RegisterDependencies()
        {
            DependencyResolver.Register<IMessageService, MessageService>();

            DependencyResolver.Register<ILoggingService, LoggingService>();

            DependencyResolver.Register<MainForm, MainForm>();
        }

        public static void SetupPresenters(IMainFormView mainFormView)
        {
            var messageService = DependencyResolver.Resolve<IMessageService>();
            var loggingService = DependencyResolver.Resolve<ILoggingService>();

            var _ = new ApplicationPresenter(mainFormView, messageService, loggingService);
        }
    }
}
