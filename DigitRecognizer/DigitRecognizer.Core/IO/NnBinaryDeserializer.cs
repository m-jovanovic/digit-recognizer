using System;
using System.Collections.Generic;
using System.IO;
using DigitRecognizer.Core.Extensions;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class NnBinaryDeserializer : NnBinaryAdapter, INnBinaryDeserializer
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly BinaryReader _reader;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fileMode"></param>
        public NnBinaryDeserializer(string filename, FileMode fileMode) : base(filename, fileMode)
        {
            _reader = new BinaryReader(FileStream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public NnFile Deserialize()
        {
            var count = DeserializeFileCount();
            Contracts.ValueGreaterThanZero(count, nameof(count));

            var fileInfo = DeserializeFileInfo();

            var fileData = DeserializeFileData(fileInfo.DataSizeInBytes);

            var file = new NnFile(fileData, fileInfo);

            return file;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NnFile> DeserializeMany()
        {
            var files = new List<NnFile>();
            var fileInfoDict = new Dictionary<int, NnFileInfo>();

            var count = DeserializeFileCount();
            Contracts.ValueGreaterThanZero(count, nameof(count));

            for (var i = 0; i < count; i++)
            {
                var fInfo = DeserializeFileInfo();

                fileInfoDict.Add(i, fInfo);
            }

            for (var i = 0; i < count; i++)
            {
                if (!fileInfoDict.TryGetValue(i, out var fileInfo))
                {
                    throw new NullReferenceException("No file info with was found for the given key.");
                }

                var fileData = DeserializeFileData(fileInfo.DataSizeInBytes);

                var file = new NnFile(fileData, fileInfo);

                files.Add(file);
            }

            return files;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private double[] DeserializeFileData(int count)
        {
            var byteData = _reader.ReadBytes(count);

            var data = byteData.ToDoubles();

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private NnFileInfo DeserializeFileInfo()
        {
            var mWidth = _reader.ReadInt32();
            var mHeight = _reader.ReadInt32();
            var bLength = _reader.ReadInt32();

            var fileInfo = new NnFileInfo(mWidth, mHeight, bLength);

            return fileInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int DeserializeFileCount()
        {
            var count = _reader.ReadInt32();

            return count;
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
                _reader?.Dispose();
            }

            _disposed = true;

            base.Dispose(true);
        }
    }
}
