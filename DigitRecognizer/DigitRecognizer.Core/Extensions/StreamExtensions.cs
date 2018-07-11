using System;
using System.IO;

namespace DigitRecognizer.Core.Extensions
{
    /// <summary>
    /// Contains extension methods for working with the <see cref="Stream"/> class.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Initialzies a <see cref="MemoryStream"/> object, from the the specified file.
        /// </summary>
        /// <param name="filename">The file path, that points to a file whose memory stream we want to get.</param>
        /// <returns>A <see cref="MemoryStream"/>.</returns>
        public static MemoryStream GetMemoryStreamFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                return default(MemoryStream);
            }

            var memoryStream = new MemoryStream();

            using (var fileStream = File.OpenRead(filename))
            {
                memoryStream.SetLength(fileStream.Length);
                fileStream.Read(memoryStream.GetBuffer(), 0, (int)fileStream.Length);
            }

            return memoryStream;
        }

        /// <summary>
        /// Sets the position within the current stream to the specified value.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> in which we are setting the position.</param>
        /// <param name="position">The position to be set in the <see cref="Stream"/>.</param>
        public static void SetPosition(this Stream stream, int position)
        {
            if (!stream.CanSeek)
            {
                return;
            }

            stream.Seek(position, SeekOrigin.Begin);
        }

        /// <summary>
        /// Sets the position within the current stream to the beginning of the stream.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> that we are resetting.</param>
        public static void Reset(this Stream stream)
        {
            stream.SetPosition(0);
        }

        /// <summary>
        /// Reads the specified number of bytes from the stream, and advances the current position of the stream.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> that we are reading from.</param>
        /// <param name="count">The number of bytes to read from the stream.</param>
        /// <returns>An array of bytes.</returns>
        public static byte[] ReadBytes(this Stream stream, int count)
        {
            if (count < 0 || count > stream.Length)
            {
                return new byte[0];
            }

            var buffer = new byte[count];

            int bytesRead = stream.Read(buffer, 0, count);

            if (bytesRead == count)
            {
                return buffer;
            }

            var smallerBuffer = new byte[bytesRead];
            Buffer.BlockCopy(buffer, 0, smallerBuffer, 0, bytesRead);
            buffer = smallerBuffer;

            return buffer;
        }
    }
}
