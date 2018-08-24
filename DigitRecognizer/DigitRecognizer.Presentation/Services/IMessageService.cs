using System.Windows.Forms;

namespace DigitRecognizer.Presentation.Services
{
    public interface IMessageService
    {
        void ShowMessage(string text, string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None);
    }
}
