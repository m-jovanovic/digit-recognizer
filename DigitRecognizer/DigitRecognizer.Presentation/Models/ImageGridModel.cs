using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using DigitRecognizer.Core.Data;
using DigitRecognizer.Core.Extensions;

namespace DigitRecognizer.Presentation.Models
{
    public class ImageGridModel
    {
        private const int Size = 28;

        private static readonly Color PaleRed = Color.FromArgb(255, 239, 153, 153);
        
        public ImageGridModel(MnistImageBatch batch, int[] predictions)
        {
            Labels = new List<int>(batch.Labels);

            Predictions = new List<int>(predictions);

            Images = new List<Image>(ProcessImages(batch.Pixels));
        }

        public ImageGridModel()
        {
            Labels = new List<int>();
            Predictions = new List<int>();
            Images = new List<Image>();
        }

        private Image[] ProcessImages(double[][] pixelArray)
        {
            var images = new Image[pixelArray.Length];

            Parallel.For(0, pixelArray.Length, (row) =>
            {
                double[] pixels = pixelArray[row];

                var bmp = new Bitmap(Size, Size);

                BitmapData data = bmp.LockBits(new Rectangle(0, 0, Size, Size), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                unsafe
                {
                    var ptr = (byte*)data.Scan0.ToPointer();

                    for (var len = 0; len < Size * Size; len++)
                    {
                        var pixel = (byte) (pixels[len] > 0.2 ? 0 : 255);
                        
                        if (Labels[row] != Predictions[row])
                        {
                            *(ptr++) = pixel == 255 ? PaleRed.B : pixel; // B
                            *(ptr++) = pixel == 255 ? PaleRed.G : pixel; // G
                            *(ptr++) = pixel == 255 ? PaleRed.R : pixel; // R
                        }
                        else
                        {
                            *(ptr++) = pixel; // B
                            *(ptr++) = pixel; // G
                            *(ptr++) = pixel; // R
                        }
                    }
                }

                bmp.UnlockBits(data);
                
                images[row] = bmp.Resize(56, 56);
            });

            return images;
        }

        public List<Image> Images { get; set; }

        public List<int> Labels { get; set; }

        public List<int> Predictions { get; set; }

        public int Count => Images.Count;

        public static ImageGridModel operator +(ImageGridModel igm1, ImageGridModel igm2)
        {
            igm1.Images.AddRange(igm2.Images);
            igm1.Labels.AddRange(igm2.Labels);
            igm1.Predictions.AddRange(igm2.Predictions);

            return igm1;
        }
    }
}
