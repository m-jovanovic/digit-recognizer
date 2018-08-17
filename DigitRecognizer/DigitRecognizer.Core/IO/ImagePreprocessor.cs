using System;
using System.Drawing;
using DigitRecognizer.Core.Data;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.IO
{
    /// <summary>
    /// Represents a preprocessor for images that will be run through the nerual network.
    /// </summary>
    public class ImagePreprocessor
    {
        private const int ImageSizeInPixels = 28;

        /// <summary>
        /// Preprocesses the specified image so that it can be fed through a neural network.
        /// The image has to be an RGB or grayscale image with 0 being black, and 255 being white.
        /// </summary>
        /// <param name="image">The image being preprocessed.</param>
        /// <returns>The flattened and clamped pixels of the preprocessed image.</returns>
        /// <remarks>
        /// First, the image is converted to a grayscale image and then a threshold is applied.
        /// The size of the box surrounding the pixels where the number is is determined.
        /// The image is then scaled to the predetermined box, and coordinates for padding are extracted.
        /// The image is then padded to again be of the required size and then it is inverted.
        /// We then calculate the center of mass, and translate the image towards the center of mass.
        /// The result is that the pixels that reperesent the number are shifted towards the center of the image.
        /// Another threshold is applied, and the pixels are then flattened and clamped.
        /// </remarks>
        public double[] Preprocess(Image image)
        {
            Image grayscale = ImageUtilities.Grayscale(image);

            Image resized = ImageUtilities.Resize(grayscale, ImageSizeInPixels, ImageSizeInPixels);

            Image grayscaleWithTreshold = ImageUtilities.Threshold(resized, 128, 0, 255);

            Box coords = ImageUtilities.DetermineBox(grayscaleWithTreshold);

            (Image, Box) imageAndPadding = ImageUtilities.ScaleToBoxAndGetPaddingCoords(grayscaleWithTreshold, coords);

            Image padded = ImageUtilities.Pad(imageAndPadding.Item1, imageAndPadding.Item2);

            Image inverted = ImageUtilities.Invert(padded);

            Point centerOfMass = ImageUtilities.CalculateCenterOfMass(inverted);

            var shiftx = (int)Math.Round(ImageSizeInPixels / 2.0 - centerOfMass.X);

            var shifty = (int)Math.Round(ImageSizeInPixels / 2.0 - centerOfMass.Y);

            Image centered = ImageUtilities.Translate(inverted, shiftx, shifty);

            Image final = ImageUtilities.Threshold(centered, 154, 0, 255, true);

            double[] data = ImageUtilities.FlattenAndClamp(final);

            return data;
        }
    }
}
