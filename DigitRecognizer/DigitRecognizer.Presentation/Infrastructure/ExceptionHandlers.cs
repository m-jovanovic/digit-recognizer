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
            var ex = (Exception) e.ExceptionObject;

            string message = $"Something went wrong.{Environment.NewLine}{Environment.NewLine}{ex}";

            DependencyResolver.Resolve<ILoggingService>().Log(ex);

            DependencyResolver.Resolve<IMessageService>().ShowMessage(message, "Unhandled Exception", icon: MessageBoxIcon.Error);
        }

        public static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string message = $"Something went wrong.{Environment.NewLine}{Environment.NewLine}{e.Exception}";

            DependencyResolver.Resolve<ILoggingService>().Log(e.Exception);

            DependencyResolver.Resolve<IMessageService>().ShowMessage(message, "Thread exception", icon: MessageBoxIcon.Information);
        }
    }
}
