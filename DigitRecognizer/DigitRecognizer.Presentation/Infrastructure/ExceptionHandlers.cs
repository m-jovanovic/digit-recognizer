using System;
using System.Threading;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Services;

namespace DigitRecognizer.Presentation.Infrastructure
{
    public static class ExceptionHandlers
    {
        public static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string message = $"Something went terribly wrong.\r\n\r\n{(Exception)e.ExceptionObject}";

            DependencyResolver.Resolve<ILoggingService>().Log((Exception)e.ExceptionObject);

            DependencyResolver.Resolve<IMessageService>().ShowMessage(message, "Unhandled Exception", icon: MessageBoxIcon.Error);
        }

        public static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string message = $"Something went terribly wrong.\r\n\r\n{e.Exception}";

            DependencyResolver.Resolve<ILoggingService>().Log(e.Exception);

            DependencyResolver.Resolve<IMessageService>().ShowMessage(message, "Thread exception", icon: MessageBoxIcon.Information);
        }
    }
}
