using System;
using System.IO;
using System.Text;

namespace DigitRecognizer.Presentation.Services
{
    public class LoggingService : ILoggingService
    {
        private const string LogFileName = "log.txt";

        public void Log(Exception e)
        {
            using (var writer = new StreamWriter(LogFileName, true))
            {
                var builder = new StringBuilder($"{DateTime.Now} - {e.Message}{Environment.NewLine}");
                
                Exception current = e;
                while (current != null)
                {
                    builder.Append(e);

                    current = current.InnerException;
                }

                builder.AppendLine();
                builder.AppendLine();

                writer.Write(builder.ToString());
            }
        }

        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            using (var writer = new StreamWriter(LogFileName, true))
            {
                var builder = new StringBuilder($"{DateTime.Now} - {message}{Environment.NewLine}");

                builder.AppendLine();

                writer.Write(builder.ToString());
            }
        }
    }
}
