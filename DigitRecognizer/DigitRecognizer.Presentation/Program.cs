using System;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new MainForm();
            var _ = new MainFormPresenter(mainForm);
            Application.Run(mainForm);
        }
    }
}
