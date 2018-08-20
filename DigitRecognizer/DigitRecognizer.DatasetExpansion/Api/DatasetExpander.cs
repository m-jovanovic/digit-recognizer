using System;
using System.Collections.Generic;
using System.Linq;
using DigitRecognizer.DatasetExpansion.Infrastructure;

namespace DigitRecognizer.DatasetExpansion.Api
{
    /// <summary>
    /// Expands the existing MNIST training dataset.
    /// </summary>
    public class DatasetExpander
    {
        /// <summary>
        /// Expands and shuffles the existing dataset with various affine transformations.
        /// </summary>
        /// <returns></returns>
        public List<MnistImage> ExpandDataset()
        {
            var result = new List<MnistImage>();

            Console.WriteLine("Reading images from disk");

            List<MnistImage> dataset = GetDataset();

            Console.WriteLine("Applying translation transformation");

            List<MnistImage> translatedImages = ApplyTranslationTransformation(dataset);

            Console.WriteLine("Applying rotation transformation");

            List<MnistImage> rotatedImages = ApplyRotationTransformation(dataset);

            result.AddRange(dataset);

            result.AddRange(translatedImages);

            result.AddRange(rotatedImages);

            Console.WriteLine("Shuffling images");

            return FisherYatesShuffle.Shuffle(result);
        }

        /// <summary>
        /// Gets the dataset form disk.
        /// </summary>
        /// <returns>A list of <see cref="MnistImage"/> objects.</returns>
        private List<MnistImage> GetDataset()
        {
            var provider = new DatasetProvider();

            return provider.LoadDataset();
        }

        /// <summary>
        /// Applies the transalation transformation to the dataset images.
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <returns>The translated images.</returns>
        private List<MnistImage> ApplyTranslationTransformation(List<MnistImage> dataset)
        {
            var result = new List<MnistImage>();

            result.AddRange(dataset.Select(x => new MnistImage(x.Label, AffineTransformation.Translate(x.Pixels, -1, 0))).ToList());

            result.AddRange(dataset.Select(x => new MnistImage(x.Label, AffineTransformation.Translate(x.Pixels, 1, 0))).ToList());

            result.AddRange(dataset.Select(x => new MnistImage(x.Label, AffineTransformation.Translate(x.Pixels, 0, -1))).ToList());

            result.AddRange(dataset.Select(x => new MnistImage(x.Label, AffineTransformation.Translate(x.Pixels, 0, 1))).ToList());

            return result;
        }

        /// <summary>
        /// Applies the rotation transformation to the dataset images.
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <returns>The rotated images.</returns>
        private List<MnistImage> ApplyRotationTransformation(List<MnistImage> dataset)
        {
            var result = new List<MnistImage>();

            var random = new Random();
            
            result.AddRange(dataset.Select(x=> new MnistImage(x.Label, AffineTransformation.Rotate(x.Pixels, random.NextDouble() > 0.5 ? 0.5 : -0.5))));
            
            return result;
        }
    }
}
