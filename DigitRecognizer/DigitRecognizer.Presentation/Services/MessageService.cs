using System.Windows.Forms;

namespace DigitRecognizer.Presentation.Services
{
    public class MessageService : IMessageService
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
