using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using DigitRecognizer.Core.Data;
using DigitRecognizer.Core.Extensions;

namespace DigitRecognizer.Presentation.Data
{
    public class ImageGridModel
    {
        private const int Size = 28;

        private static readonly Color PaleRed = Color.FromArgb(255, 239, 153, 153);
        
        public ImageGridModel(MnistImageBatch batch, int[] predictions)
        {
            Count = predictions.Length;
            Labels = batch.Labels;
            Predictions = predictions;
            Images = ProcessImages(batch.Pixels);
        }

        public ImageGridModel()
        {
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

        public Image[] Images { get; set; }

        public int[] Labels { get; set; }

        public int[] Predictions { get; set; }

        public int Count { get; set; }
    }
}
