using System;
using System.Diagnostics;

namespace DigitRecognizer.Core.Utilities
{
    /// <summary>
    /// Contains utility methods for printing in a console window.
    /// </summary>
    public static class ConsoleUtility
    {
        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteLine(string message)
        {
            #if DEBUG
            Debug.WriteLine(message);
            #endif
            Console.WriteLine(message);
        }
    }
}
