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
        /// Checks if the specified value is greater than zero.
        /// </summary>
        /// <param name="val">The value that is being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void ValueGreaterThanZero(int val, string paramName)
        {
            if (val < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, "Value can not be less than zero.");
            }
        }

        /// <summary>
        /// Checks if the specified value is greater than zero.
        /// </summary>
        /// <param name="val">The value that is being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void ValueGreaterThanZero(double val, string paramName)
        {
            if (val < 0.0)
            {
                throw new ArgumentOutOfRangeException(paramName, "Value can not be less than zero.");
            }
        }

        /// <summary>
        /// Checks if the specified value is within the specified bounds.
        /// </summary>
        /// <param name="val">The value that is being checked.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upepr bound.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void ValueWithinBounds(int val, int lowerBound, int upperBound, string paramName)
        {
            if (val < lowerBound || val > upperBound)
            {
                throw new ArgumentOutOfRangeException(paramName, "The value is out of valid bounds.");
            }
        }

        /// <summary>
        /// Checks if the specified values match.
        /// </summary>
        /// <param name="val1">The first value.</param>
        /// <param name="val2">The second value.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void ValuesMatch(int val1, int val2, string paramName)
        {
            if (val1 != val2)
            {
                throw new ArgumentOutOfRangeException(paramName, "The values do not match.");
            }
        }

        /// <summary>
        /// Checks if the specified file exists.
        /// </summary>
        /// <param name="filename">The file being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void FileExists(string filename, string paramName)
        {
            if (!File.Exists(filename))
            {
                throw new ArgumentException($"File was not found or was not accessible: {filename}", paramName);
            }
        }

        /// <summary>
        /// Checks if the specified file has an extension.
        /// </summary>
        /// <param name="filename">The file being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void FileHasExtension(string filename, string paramName)
        {
            if (!Path.HasExtension(filename))
            {
                throw new ArgumentException("File name is invalid.", paramName);
            }
        }

        /// <summary>
        /// Checks if the specified file has a valid extension.
        /// </summary>
        /// <param name="filename">The file being checked.</param>
        /// <param name="extension">The file extension that is expected.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void FileExtensionValid(string filename, string extension, string paramName)
        {
            if (Path.GetExtension(filename) != extension)
            {
                throw new ArgumentException($"File extension '{Path.GetExtension(filename)}' is invalid.", paramName);
            }
        }

        /// <summary>
        /// Checks that the specified string is not null or empty.
        /// </summary>
        /// <param name="str">The string that is being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void StringNotNullOrEmpty(string str, string paramName)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("String can not be null or empty.", paramName);
            }
        }

        /// <summary>
        /// Checks if the specified is null.
        /// </summary>
        /// <param name="val">The value being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void ValueNotNull(object val, string paramName)
        {
            if (val == null)
            {
                throw new ArgumentNullException(paramName, "Value can not be null.");
            }
        }

        /// <summary>
        /// Checks if the specified value is not null.
        /// </summary>
        /// <param name="val">The value being checked.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static void ValueIsNull(object val, string paramName)
        {
            if (val != null)
            {
                throw new ArgumentException("Value must be null.", paramName);
            }
        }
    }
}
