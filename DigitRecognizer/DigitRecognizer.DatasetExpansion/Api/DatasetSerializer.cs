using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DigitRecognizer.DatasetExpansion.Infrastructure;

namespace DigitRecognizer.DatasetExpansion.Api
{
    /// <summary>
    /// Represents a serializer for an MNIST dataset.
    /// </summary>
    public class DatasetSerializer
    {
        private const int LabelMagicNumber = 0x00000801;

        private const int ImageMagicNumber = 0x00000803;

        private const int Length = 28;

        /// <summary>
        /// Serializes the specified dataset to the specified files.
        /// </summary>
        /// <param name="labelsFilename">The name of the file with labels data.</param>
        /// <param name="imagesFilename">The name of the file with images data.</param>
        /// <param name="dataset">The dataset.</param>
        public void SerializeDataset(string labelsFilename, string imagesFilename, List<MnistImage> dataset)
        {
            Console.WriteLine("Ensuring file integrity");

            EnsureFileIntegrity(labelsFilename);
            EnsureFileIntegrity(imagesFilename);

            // The dataset size must be MSB first.
            int datasetSize = BitConverter.ToInt32(BitConverter.GetBytes(dataset.Count).Reverse().ToArray(), 0);

            using (var lblWriter = new BinaryWriter(File.Open(labelsFilename, FileMode.Create, FileAccess.Write)))
            using (var imgWriter = new BinaryWriter(File.Open(imagesFilename, FileMode.Create, FileAccess.Write)))
            {
                // Write the magic number and dataset size to the file.
                lblWriter.Write(LabelMagicNumber);

                lblWriter.Write(datasetSize);

                // Write the magic number and dataset size to the file.
                imgWriter.Write(ImageMagicNumber);

                imgWriter.Write(datasetSize);

                imgWriter.Write(Length);

                imgWriter.Write(Length);

                // Write bytes of each image to appropriate files.
                foreach (MnistImage img in dataset)
                {
                    lblWriter.Write(img.Label);

                    imgWriter.Write(img.Pixels);
                }
            }
        }

        /// <summary>
        /// Ensures the integrity of the file with the specified file name.
        /// </summary>
        /// <param name="filename">The filename.</param>
        private void EnsureFileIntegrity(string filename)
        {
            const int retryCount = 5;

            for (var i = 0; i < retryCount; i++)
            {
                try
                {
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }

                    return;
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            throw new Exception($"Failed to delete file {filename}");
        }
    }
}
