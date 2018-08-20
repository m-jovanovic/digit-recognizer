using System.Collections.Generic;
using DigitRecognizer.Core.IO;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.DatasetExpansion.Infrastructure
{
    /// <summary>
    /// Loads the current dataset from disk.
    /// </summary>
    public class DatasetProvider
    {
        private const int ImagePixelCount = 784;
        private const int DatasetSize = 60000;

        private static readonly string TrainImagesFilePath = DirectoryHelper.TrainImagesPath;

        private static readonly string TrainLabelsFilePath = DirectoryHelper.TrainLabelsPath;

        /// <summary>
        /// Lodas the MNIST training dataset from disk.
        /// </summary>
        /// <returns>The current dataset.</returns>
        public List<MnistImage> LoadDataset()
        {
            var result = new List<MnistImage>();

            using (var labelReader = new MemoryStreamReader(TrainLabelsFilePath, LabelReader.InitialOffset))
            using (var pixelReader = new MemoryStreamReader(TrainImagesFilePath, PixelReader.InitialOffset))
            {
                for (var i = 0; i < DatasetSize; i++)
                {
                    byte label = labelReader.Read(1)[0];

                    byte[] pixels = pixelReader.Read(ImagePixelCount);

                    var img = new MnistImage(label, pixels);

                    result.Add(img);
                }
            }

            return result;
        }
    }
}
