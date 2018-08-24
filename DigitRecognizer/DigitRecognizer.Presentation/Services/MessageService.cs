using System.Windows.Forms;

namespace DigitRecognizer.Presentation.Services
{
    public class MessageService : IMessageService
    {
        public void ShowMessage(string text, string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            MessageBox.Show(text, caption, buttons, icon);
        }
    }
}
