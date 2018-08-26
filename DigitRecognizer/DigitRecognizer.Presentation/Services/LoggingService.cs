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
                    builder.Append(e.StackTrace);

                    current = current.InnerException;
                }

                builder.AppendLine();

                writer.Write(builder.ToString());
            }
        }
    }
}
