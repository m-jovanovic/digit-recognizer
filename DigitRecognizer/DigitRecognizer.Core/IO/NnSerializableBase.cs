using System;
using System.IO;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// An abstract base class for serialization of neural network files.
    /// </summary>
    public abstract class NnSerializableBase : IDisposable
    {
        /// <summary>
        /// The file extension of a neural network file.
        /// </summary>
        private const string NnFileExtension = ".nn";
        
        /// <summary>
        /// The internal <see cref="System.IO.FileStream"/> object.
        /// </summary>
        protected readonly FileStream FileStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="NnSerializableBase"/> class.
        /// </summary>
        /// <param name="filename">The name of the file, used for opening a file stream.</param>
        /// <param name="fileMode">The file acces mode of the adapter.</param>
        protected NnSerializableBase(string filename, FileMode fileMode)
        {
            Contracts.StringNotNullOrEmpty(filename, nameof(filename));
            Contracts.FileHasExtension(filename, nameof(filename));
            Contracts.FileExtensionValid(filename, NnFileExtension, nameof(filename));
           // Contracts.FileExists(filename, nameof(filename));

            FileStream = File.Open(filename, fileMode, FileAccess.ReadWrite);
        }

        /// <summary>
        /// Indicates if the disposing operation has been completed or not.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Releases all resources used by the <see cref="NnSerializableBase"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="NnSerializableBase"/>.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                FileStream?.Dispose();
            }

            _disposed = true;
        }
    }
}
