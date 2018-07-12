using System;
using System.IO;

namespace DigitRecognizer.Core.Utilities
{
    /// <summary>
    /// Contains contracts that have to be met. Used for validation purposes.
    /// </summary>
    public static class Contracts
    {
        /// <summary>
        /// Assures that the specified value is greater than zero.
        /// </summary>
        /// <param name="val">The value that is being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ValueGreaterThanZero(int val, string paramName)
        {
            if (val < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, "Value can not be less than zero.");
            }
        }

        /// <summary>
        /// Assures that the specified value is greater than zero.
        /// </summary>
        /// <param name="val">The value that is being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ValueGreaterThanZero(double val, string paramName)
        {
            if (val < 0.0)
            {
                throw new ArgumentOutOfRangeException(paramName, "Value can not be less than zero.");
            }
        }

        /// <summary>
        /// Assures that the specified value is within the specified bounds.
        /// </summary>
        /// <param name="val">The value that is being checked.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upepr bound.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ValueWithinBounds(int val, int lowerBound, int upperBound, string paramName)
        {
            if (val < lowerBound || val > upperBound)
            {
                throw new ArgumentOutOfRangeException(paramName, "The value is out of valid bounds.");
            }
        }

        /// <summary>
        /// Assures that the specified values match.
        /// </summary>
        /// <param name="val1">The first value.</param>
        /// <param name="val2">The second value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ValuesMatch(int val1, int val2, string paramName)
        {
            if (val1 != val2)
            {
                throw new ArgumentOutOfRangeException(paramName, "The values do not match.");
            }
        }

        /// <summary>
        /// Assures that the specified file exists.
        /// </summary>
        /// <param name="filename">The file being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void FileExists(string filename, string paramName)
        {
            if (!File.Exists(filename))
            {
                throw new ArgumentException($"File was not found or was not accessible: {filename}", paramName);
            }
        }

        /// <summary>
        /// Assures that the specified filename has a file extension.
        /// </summary>
        /// <param name="filename">The file being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void FileHasExtension(string filename, string paramName)
        {
            if (!Path.HasExtension(filename))
            {
                throw new ArgumentException("File name is invalid.", paramName);
            }
        }

        /// <summary>
        /// Assures that the specified filename has a valid file extension.
        /// </summary>
        /// <param name="filename">The file being checked.</param>
        /// <param name="extension">The file extension that is expected.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void FileExtensionValid(string filename, string extension, string paramName)
        {
            if (Path.GetExtension(filename) != extension)
            {
                throw new ArgumentException($"File extension '{Path.GetExtension(filename)}' is invalid.", paramName);
            }
        }

        /// <summary>
        /// Assures that the specified string is not null or empty.
        /// </summary>
        /// <param name="str">The string that is being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void StringNotNullOrEmpty(string str, string paramName)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("String can not be null or empty.", paramName);
            }
        }

        /// <summary>
        /// Assures that the specified value is null.
        /// </summary>
        /// <param name="val">The value being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void ValueIsNull(object val, string paramName)
        {
            if (val != null)
            {
                throw new ArgumentException("Value must be null.", paramName);
            }
        }

        /// <summary>
        /// Assures that the specified value is not null.
        /// </summary>
        /// <param name="val">The value being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValueNotNull(object val, string paramName)
        {
            if (val == null)
            {
                throw new ArgumentNullException(paramName, "Value can not be null.");
            }
        }
    }
}
