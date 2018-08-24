using System;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Infrastructure;
using DigitRecognizer.Presentation.Views.Implementations;

namespace DigitRecognizer.Presentation
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Startup.RegisterDependencies();

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Option to continue with execution.
            Application.ThreadException += ExceptionHandlers.ApplicationOnThreadException;
            
            // Application will terminate.
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandlers.CurrentDomainOnUnhandledException;

            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);
            
            var mainForm = DependencyResolver.Resolve<MainForm>();

            Startup.SetupPresenters(mainForm);

            Application.Run(mainForm);
        }
    }
}
