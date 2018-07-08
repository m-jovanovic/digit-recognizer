using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.Core.Extensions;

namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class NnBinarySerializer : NnBinaryAdapter, INnBinarySerializer
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly BinaryWriter _writer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fileMode"></param>
        public NnBinarySerializer(string filename, FileMode fileMode) : base(filename, fileMode)
        {
            _writer = new BinaryWriter(FileStream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public void Serialize(NnFile file)
        {
            SerializeFileCount(1);
            SerializeFileInfo(file.FileInfo);
            SerializeFileData(file.FileData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        public void Serialize(IEnumerable<NnFile> files)
        {
            var nnFiles = files as NnFile[] ?? files.ToArray();
            var count = nnFiles.Length;

            SerializeFileCount(count);

            for (var i = 0; i < count; i++)
            {
                SerializeFileInfo(nnFiles[i].FileInfo);
            }

            for (var i = 0; i < count; i++)
            {
                SerializeFileData(nnFiles[i].FileData);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        private void SerializeFileCount(int count)
        {
            _writer.Write(count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        private void SerializeFileInfo(NnFileInfo fileInfo)
        {
            _writer.Write(fileInfo.WeightMatrixWidth);
            _writer.Write(fileInfo.WeightMatrixHeight);
            _writer.Write(fileInfo.BiasLength);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        private void SerializeFileData(double[] fileData)
        {
            _writer.Write(fileData.ToBytes());
        }

        /// <summary>
        /// Indicates if the disposing operation has been completed or not.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Releases all resources used by the <see cref="NnBinarySerializer"/>.
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="NnBinarySerializer"/>.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _writer?.Dispose();
            }

            _disposed = true;

            base.Dispose(true);
        }
    }
}
