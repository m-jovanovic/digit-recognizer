using System.Drawing;
using DigitRecognizer.Core.Utilities;

namespace DigitRecognizer.Core.Extensions
{
    /// <summary>
    /// Contains extensions methods for the <see cref="Image"/> class.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Resizes the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image being resized.</param>
        /// <param name="width">The new width in pixels.</param>
        /// <param name="height">The new height in pixels.</param>
        /// <returns>The resized image.</returns>
        public static Image Resize(this Image image, int width, int height)
        {
            return ImageUtilities.Resize(image, width, height);
        }
    }
}
