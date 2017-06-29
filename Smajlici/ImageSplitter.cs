using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace Smajlici
{
    static class ImageSplitter
    {

        public static BitmapImage[] SplitImage(Uri wholeImage)
        {

            Bitmap BM = CreateBM(wholeImage);
            int partSize = ((int)(BM.Width) / 3);


            int index = 0;
            BitmapImage[] splittedImage = new BitmapImage[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    splittedImage[index++] = ConvToBTI(BM.Clone(new Rectangle(j * partSize, i * partSize, partSize, partSize),
                        BM.PixelFormat));
                }
            }
            return splittedImage;

        }

        private static BitmapImage ConvToBTI(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                memory.Dispose();
                return bitmapImage;
            }
        }

        private static Bitmap CreateBM(Uri uri)
        {
            BitmapImage BMI = new BitmapImage(uri);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapImage)BMI));
            MemoryStream stream = new MemoryStream();
            encoder.Save(stream);
            stream.Flush();
            Bitmap image = new System.Drawing.Bitmap(stream);

            return image;
        }
    }
}
