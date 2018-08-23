using System;
using System.Threading;
using System.Windows.Forms;
using DigitRecognizer.Presentation.Presenters;
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
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Option to continue with execution.
            Application.ThreadException += ApplicationOnThreadException;
            
            // Application will terminate.
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);
            
            var mainForm = new MainForm();

            var _ = new ApplicationPresenter(mainForm);

            Application.Run(mainForm);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string message = $@"Something went terribly wrong.\r\n{(Exception)e.ExceptionObject}";

            // Log to file

            MessageBox.Show(message, @"Unexpected error");
        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string message = $@"Something went terribly wrong.\r\n{e.Exception}";

            // Log to file

            MessageBox.Show(message, @"Unexpected error");
        }
    }
}
