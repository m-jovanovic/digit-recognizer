using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using DigitRecognizer.Core.Data;

namespace DigitRecognizer.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class ImageUtilities
    {
        public static Image Resize(Image srcImage, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(srcImage.HorizontalResolution, srcImage.VerticalResolution);

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
                    graphics.DrawImage(srcImage, destRect, 0, 0, srcImage.Width, srcImage.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Image Grayscale(Image image)
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

        public static Image Threshold(Image image, int threshold)
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
                            p[0] = p[0] > threshold ? (byte)255 : (byte)0;

                            p[1] = p[1] > threshold ? (byte)255 : (byte)0;

                            p[2] = p[2] > threshold ? (byte)255 : (byte)0;

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

        public static Image SoftThreshold(Image image, int threshold)
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
                            p[0] = p[0] > threshold ? p[0] : (byte)0;

                            p[1] = p[1] > threshold ? p[1] : (byte)0;

                            p[2] = p[2] > threshold ? p[2] : (byte)0;

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

        public static double[] Flatten(Image image)
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

        public static Box DetermineBox(Image image)
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

        public static (Image, Box) ScaleToBoxAndGetPaddingCoords(Image image, Box boxCoords)
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
                Top = (int)Math.Ceiling((28 - rows) / 2.0),
                Bottom = (int)Math.Floor((28 - rows) / 2.0),
                Left = (int)Math.Ceiling((28 - cols) / 2.0),
                Right = (int)Math.Floor((28 - cols) / 2.0)
            };

            return (scaled, padding);
        }

        public static Image Pad(Image image, Box coords)
        {
            var bmp = new Bitmap(28, 28);

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

        public static Image Invert(Image image)
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

        public static Point CalculateCenterOfMass(Image image)
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

        public static Image Translate(Image image, int x, int y)
        {
            var bmp = new Bitmap(28, 28);

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
                        bmp.SetPixel(Math.Abs((i + x) % 28), Math.Abs((j + y) % 28), tempBmp.GetPixel(i, j));
                    }
                }
            }

            return bmp;
        }

        public static double[] Preprocess(this Image image)
        {
            Image grayscale = Grayscale(image);
            Image resized = Resize(grayscale, 28, 28);
            Image grayscaleWithTreshold = Threshold(resized, 128);
            Box coords = DetermineBox(grayscaleWithTreshold);
            (Image, Box) imageAndPadding = ScaleToBoxAndGetPaddingCoords(grayscaleWithTreshold, coords);
            Image padded = Pad(imageAndPadding.Item1, imageAndPadding.Item2);
            Image inverted = Invert(padded);
            Point centerOfMass = CalculateCenterOfMass(inverted);
            var shiftx = (int)Math.Round(28 / 2.0 - centerOfMass.X);
            var shifty = (int)Math.Round(28 / 2.0 - centerOfMass.Y);
            Image centered = Translate(inverted, shiftx, shifty);
            Image final = SoftThreshold(centered, 154);
            double[] data = Flatten(final);
            return data;
        }
    }
}
