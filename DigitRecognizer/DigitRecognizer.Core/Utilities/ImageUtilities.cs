using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using DigitRecognizer.Core.Data;

namespace DigitRecognizer.Core.Utilities
{
    /// <summary>
    /// Contains utility methods for commonly required image operations.
    /// </summary>
    public static class ImageUtilities
    {
        private const int ImageSizeInPixels = 28;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="windowSize"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IEnumerable<BoundingBox> SlidingWindow(Image image, Size windowSize, int step)
        {
            var boxes = new List<BoundingBox>();

            for (var y = 0; y < image.Height && y + windowSize.Height < image.Height; y += step)
            {
                for (var x = 0; x < image.Width && x + windowSize.Width < image.Width; x += step)
                {
                    Image img = SampleImageAtLocation(image, x, y, windowSize);

                    if (img.Width == windowSize.Width && img.Height == windowSize.Height)
                    {
                        boxes.Add(new BoundingBox
                        {
                            Image = img,
                            X = x,
                            Y = y
                        });
                    }
                }
            }
            
            return boxes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="windowSize"></param>
        /// <returns></returns>
        private static Image SampleImageAtLocation(Image image, int x, int y, Size windowSize)
        {
            var sample = new Bitmap(windowSize.Width, windowSize.Height);

            using (var bmp = new Bitmap(image))
            {
                for (int i = y; i < y + windowSize.Height && i < bmp.Height; i++)
                {
                    for (int j = x; j < x + windowSize.Width && j < bmp.Width; j++)
                    {
                        try
                        {
                            sample.SetPixel(j - x, i - y, bmp.GetPixel(j, i));
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine($"x: {i}, y: {j}");
                        }
                    }
                }
            }
                //using (var bmp = new Bitmap(image))
                //{
                //    BitmapData bmpData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadWrite,
                //        PixelFormat.Format24bppRgb);

                //    try
                //    {
                //        unsafe
                //        {
                //            byte* p = (byte*)bmpData.Scan0 + y * bmpData.Stride + x * 3;

                //            for (int i = y; i < y + windowSize.Height; i++)
                //            {
                //                for (int j = x; j < x + windowSize.Width; j++)
                //                {
                //                    sample.SetPixel(j - x, i - y, Color.FromArgb(p[2],p[1],p[0]));

                //                    p += 3;
                //                }

                //                p += bmpData.Stride;
                //            }
                //        }
                //    }
                //    finally
                //    {
                //        bmp.UnlockBits(bmpData);
                //    }
                //}

                return sample;
        }

        /// <summary>
        /// Resizes the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image being resized.</param>
        /// <param name="width">The new width in pixels.</param>
        /// <param name="height">The new height in pixels.</param>
        /// <returns>The resized image.</returns>
        public static Image Resize(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// Converts the specified image to a grayscale image using the avergaging method of converting RGB to grayscale.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The grayscale representation of the original image.</returns>
        internal static Image Grayscale(Image image)
        {
            var bmp = new Bitmap(image);
            BitmapData bmpData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);
            try
            {
                unsafe
                {
                    var p = (byte*)bmpData.Scan0.ToPointer();
                    int offset = bmpData.Stride - 3 * bmp.Width;
                    for (var x = 0; x < bmp.Height; x++)
                    {
                        for (var y = 0; y < bmp.Width; y++)
                        {
                            var avg = (byte)((p[0] + p[1] + p[2]) / 3.0);
                            p[0] = avg;
                            p[1] = avg;
                            p[2] = avg;

                            p += 3;
                        }

                        p += offset * 3;
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }

            return bmp;
        }

        /// <summary>
        /// Applies the specified threshold to the pixels of the image.
        /// Values under the threshold are replaced with the min value.
        /// Values above the threshold are replaced with the max value, or the original value if the <paramref name="isSoftTreshold"/> is true.
        /// </summary>
        /// <param name="image">The image being altered.</param>
        /// <param name="threshold">The treshold value.</param>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <param name="isSoftTreshold">true if the max value should be the original value.</param>
        /// <returns>The image with the treshold applied.</returns>
        internal static Image Threshold(Image image, int threshold, int minValue, int maxValue,
            bool isSoftTreshold = false)
        {
            if (minValue < 0) minValue = 0;
            if (maxValue > 255) maxValue = 255;

            if (minValue > maxValue) throw new ArgumentException("Minimum value can not be greater than the maximum value", nameof(minValue));
            if (maxValue < minValue) throw new ArgumentException("Maximum value can not be smaller than the minimum value", nameof(maxValue));
            
            Contracts.ValueWithinBounds(threshold, 0, 255, nameof(threshold));

            var bmp = new Bitmap(image);
            BitmapData bmpData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);
            try
            {
                unsafe
                {
                    var p = (byte*)bmpData.Scan0.ToPointer();
                    int offset = bmpData.Stride - 3 * bmp.Width;
                    for (var x = 0; x < bmp.Height; x++)
                    {
                        for (var y = 0; y < bmp.Width; y++)
                        {
                            p[0] = p[0] > threshold ? 
                                isSoftTreshold ? 
                                    p[0] : (byte)maxValue : 
                                (byte)minValue;

                            p[1] = p[1] > threshold ?
                                isSoftTreshold ?
                                    p[1] : (byte)maxValue :
                                (byte)minValue;

                            p[2] = p[2] > threshold ?
                                isSoftTreshold ?
                                    p[2] : (byte)maxValue :
                                (byte)minValue;

                            p += 3;
                        }

                        p += offset * 3;
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }

            return bmp;
        }

        /// <summary>
        /// Flattens all the pixels of the image to an array and clamps the values to be [0-1].
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The flattened pixels clamped to the range [0-1].</returns>
        internal static double[] FlattenAndClamp(Image image)
        {
            var result = new double[image.Width * image.Height];
            using (var bmp = new Bitmap(image))
            {
                BitmapData bmpData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadWrite,
                    PixelFormat.Format24bppRgb);
                try
                {
                    unsafe
                    {
                        var p = (byte*)bmpData.Scan0.ToPointer();
                        int offset = bmpData.Stride - 3 * bmp.Width;
                        var counter = 0;
                        for (var x = 0; x < bmp.Height; x++)
                        {
                            for (var y = 0; y < bmp.Width; y++)
                            {
                                result[counter++] = p[0] / 255.0;

                                p += 3;
                            }

                            p += offset * 3;
                        }
                    }
                }
                finally
                {
                    bmp.UnlockBits(bmpData);
                }
            }

            return result;
        }

        /// <summary>
        /// Determines the coordinates of the box surrounding the raw digit in the image.
        /// <para>
        /// The idea is to remove all rows and columns that contain only white pixels.
        /// </para>
        /// </summary>
        /// <param name="image">The image being processed.</param>
        /// <returns>The coordinates of the outer box of the image.</returns>
        internal static Box DetermineBox(Image image)
        {
            var result = new Box();
            var bmp = new Bitmap(image);
            int sum = image.Width * 255;

            result.Top = -1;
            while (sum == image.Width * 255)
            {
                result.Top++;
                sum = 0;
                for (var x = 0; x < image.Width; x++)
                {
                    sum += bmp.GetPixel(x, result.Top).B;
                }
            }

            sum = image.Width * 255;
            result.Bottom = image.Height;
            while (sum == image.Width * 255)
            {
                result.Bottom--;
                sum = 0;
                for (var x = 0; x < image.Width; x++)
                {
                    sum += bmp.GetPixel(x, result.Bottom).B;
                }
            }

            sum = image.Height * 255;
            result.Left = -1;
            while (sum == image.Height * 255)
            {
                result.Left++;
                sum = 0;
                for (var y = 0; y < image.Height; y++)
                {
                    sum += bmp.GetPixel(result.Left, y).B;
                }
            }

            sum = image.Height * 255;
            result.Right = image.Width;
            while (sum == image.Height * 255)
            {
                result.Right--;
                sum = 0;
                for (var y = 0; y < image.Height; y++)
                {
                    sum += bmp.GetPixel(result.Right, y).B;
                }
            }

            return result;
        }

        /// <summary>
        /// Scales the specified image to a 20x20 box, based on the specified coordinates.
        /// </summary>
        /// <param name="image">The image being scaled.</param>
        /// <param name="boxCoords">The box with coordinates used for scaling.</param>
        /// <returns>The scaled image, and the box containing padding coordinates.</returns>
        internal static (Image, Box) ScaleToBoxAndGetPaddingCoords(Image image, Box boxCoords)
        {
            int rows = boxCoords.Bottom - boxCoords.Top + 1;
            int cols = boxCoords.Right - boxCoords.Left + 1;

            double factor;
            if (rows > cols)
            {
                factor = 20.0 / rows;
                rows = 20;
                cols = (int)Math.Round(cols * factor);
            }
            else
            {
                factor = 20.0 / cols;
                cols = 20;
                rows = (int)Math.Round(rows * factor);
            }
            Image scaled = Resize(image, cols, rows);

            var padding = new Box
            {
                Top = (int)Math.Ceiling((ImageSizeInPixels - rows) / 2.0),
                Bottom = (int)Math.Floor((ImageSizeInPixels - rows) / 2.0),
                Left = (int)Math.Ceiling((ImageSizeInPixels - cols) / 2.0),
                Right = (int)Math.Floor((ImageSizeInPixels - cols) / 2.0)
            };

            return (scaled, padding);
        }

        /// <summary>
        /// Pads the image with blank rows, using the specified coordinates.s
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="coords">The coordinates used for padding.</param>
        /// <returns>The padded image.</returns>
        internal static Image Pad(Image image, Box coords)
        {
            var bmp = new Bitmap(ImageSizeInPixels, ImageSizeInPixels);

            for (var x = 0; x < bmp.Width; x++)
            {
                for (var y = 0; y < bmp.Height; y++)
                {
                    bmp.SetPixel(x, y, Color.White);
                }
            }

            using (var tempBmp = new Bitmap(image))
            {
                int xOffset = coords.Left;
                int yOffset = coords.Top;
                for (int x = coords.Left; x < xOffset + tempBmp.Width; x++)
                {
                    for (int y = coords.Top; y < yOffset + tempBmp.Height; y++)
                    {
                        bmp.SetPixel(x, y, tempBmp.GetPixel(x - xOffset, y - yOffset));
                    }
                }
            }

            return bmp;
        }

        /// <summary>
        /// Inverts the image pixels.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>The inverted image.</returns>
        internal static Image Invert(Image image)
        {
            var bmp = new Bitmap(image);
            BitmapData bmpData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);
            try
            {
                unsafe
                {
                    var p = (byte*)bmpData.Scan0.ToPointer();
                    int offset = bmpData.Stride - 3 * bmp.Width;
                    for (var x = 0; x < bmp.Height; x++)
                    {
                        for (var y = 0; y < bmp.Width; y++)
                        {
                            p[0] = (byte)(255 - p[0]);
                            p[1] = (byte)(255 - p[1]);
                            p[2] = (byte)(255 - p[2]);

                            p += 3;
                        }

                        p += offset * 3;
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }

            return bmp;
        }

        /// <summary>
        /// Calculates the center of mass of the specified image.
        /// </summary>
        /// <param name="image">The grayscale image.</param>
        /// <returns>The point representing the center of mass.</returns>
        internal static Point CalculateCenterOfMass(Image image)
        {
            using (var bmp = new Bitmap(image))
            {
                var totalmass = 0.0;
                var totalX = 0.0;
                var totalY = 0.0;
                for (var x = 0; x < bmp.Width; x++)
                {
                    for (var y = 0; y < bmp.Height; y++)
                    {
                        int mass = bmp.GetPixel(x, y).B;
                        totalmass += mass;
                        totalX += x * mass;
                        totalY += y * mass;
                    }
                }

                double centerX = totalX / totalmass;
                double centerY = totalY / totalmass;

                var result = new Point((int)Math.Round(centerX), (int)Math.Round(centerY));

                return result;
            }
        }

        /// <summary>
        /// Translates the image in the specified direction.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="x">The value indicating how much to shift in the horizontal direction.</param>
        /// <param name="y">The value indicating how much to shift in the vertical direction.</param>
        /// <returns>The trasnlated image.</returns>
        internal static Image Translate(Image image, int x, int y)
        {
            var bmp = new Bitmap(ImageSizeInPixels, ImageSizeInPixels);

            for (var i = 0; i < bmp.Width; i++)
            {
                for (var j = 0; j < bmp.Height; j++)
                {
                    bmp.SetPixel(i, j, Color.Black);
                }
            }

            using (var tempBmp = new Bitmap(image))
            {
                for (var i = 0; i < tempBmp.Width; i++)
                {
                    for (var j = 0; j < tempBmp.Height; j++)
                    {
                        bmp.SetPixel(Math.Abs((i + x) % ImageSizeInPixels), Math.Abs((j + y) % ImageSizeInPixels), tempBmp.GetPixel(i, j));
                    }
                }
            }

            return bmp;
        }
    }
}
