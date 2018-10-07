using System;

namespace DigitRecognizer.Presentation.Services
{
    public interface ILoggingService
    {
        void Log(Exception e);

        void Log(string message);
    }
}
