using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Smajlici
{
    static class ImageSplitter
    {

        public static ImagePart[] SplitImage(Uri wholeImage,bool isDefault)
        {

            Bitmap bitMap = CreateBitMap(wholeImage);
            
            int partSize = (bitMap.Width  / 3);

            if (partSize > 0)
            {
                int index = 0;
                ImagePart[] splittedImage = new ImagePart[9];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Bitmap bmChunk = bitMap.Clone(new Rectangle(j * partSize, i * partSize, partSize, partSize),
                            bitMap.PixelFormat);
                        if (isDefault)
                        {
                            splittedImage[index] = new ImagePart(index, ConvertToBitmapImage(bmChunk), null);
                        }
                        else splittedImage[index] = new ImagePart(index, ConvertToBitmapImage(bmChunk), PixelRecognize(bmChunk));
                        index++;
                    }
                }
                return splittedImage;
            }
            return null;
        }

        private static BitmapImage ConvertToBitmapImage(Bitmap bitmap)
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

        private static Bitmap CreateBitMap(Uri uri)
        {
            BitmapImage bitmapImage = new BitmapImage(uri);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            MemoryStream stream = new MemoryStream();
            encoder.Save(stream);
            stream.Flush();
            Bitmap image = new Bitmap(stream);

            return image;
        }

        private static ImageChunk[] PixelRecognize(Bitmap bitmap)
        {
            ImageChunk[] chunkType = new ImageChunk[4];
            Point[] colorRelativeSamples = {new Point(29, 49), new Point(2, 74), new Point(2, 24)};
            Point[] eyeRelativeSamples = { new Point(15, 58), new Point(15, 40) };
            Point[] smileRelativeSamples = { new Point(10, 69), new Point(7, 29), new Point(24, 49) };

            for (int i = 0; i < chunkType.Length; i++)
            {
                ImageChunk.ImageChunkColor? iColor = null;
                ImageChunk.ImageChunkFace? iFace = null;

                if (i == 3)
                {
                    Console.Write("");
                }

                iColor = CheckColorFromSamples(colorRelativeSamples, bitmap);
                if (iColor != ImageChunk.ImageChunkColor.Black && iColor != null)
                {
                    if (CheckColorFromSamples(smileRelativeSamples,bitmap) == ImageChunk.ImageChunkColor.Black)
                    {
                        iFace = ImageChunk.ImageChunkFace.Smile;
                    }
                    else if (CheckColorFromSamples(eyeRelativeSamples,bitmap) == ImageChunk.ImageChunkColor.Black)
                    {
                        iFace = ImageChunk.ImageChunkFace.Eyes;
                    }

                }

                if (iColor != null && iFace != null)
                {
                    chunkType[i] = new ImageChunk(iColor.Value, iFace.Value);
                    bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else
                {
                    throw new Exception("Nelze rozeznat obrazek.");
                }
                
                
            }
            return chunkType;
        }

        private static ImageChunk.ImageChunkColor? ToColorEnum(Color color)
        {
            ImageChunk.ImageChunkColor iColor;

            int r = color.R;
            int g = color.G;
            int b = color.B;
            if (r > (g + 50) && (r > b + 50))
            {
                iColor = ImageChunk.ImageChunkColor.Red;
            }
            else if (g > (r + 50) && g > (b + 50))
            {
                iColor = ImageChunk.ImageChunkColor.Green;
            }
            else if (Math.Abs(g - b) <100 && b - r > 50) 
            {
                iColor = ImageChunk.ImageChunkColor.Blue;
            }
            else if (Math.Abs(r - g) < 25 && r > (b + 50))
            {
                iColor = ImageChunk.ImageChunkColor.Yellow;
            }
            else if (r < 30 && g < 30 && b < 30)
            {
                iColor = ImageChunk.ImageChunkColor.Black;
            }
            else
            {
                return null;
            }
            return iColor;

        }


        private static int FromPercentage(int percentagePosition,int widthOrHeight)
        {
            double OnePercent = widthOrHeight * 0.01;
            double result = (OnePercent*percentagePosition);
            return (int)Math.Round(result);
        }

        private static ImageChunk.ImageChunkColor? CheckColorFromSamples(Point[] samples,Bitmap bitmap)
        {
            ImageChunk.ImageChunkColor? iColor = null;
            

            foreach (Point t in samples)
            {
                int absolutePointX = FromPercentage(t.X, bitmap.Width);
                int absolutePointY = FromPercentage(t.Y, bitmap.Height);
                Color c = bitmap.GetPixel(absolutePointX,absolutePointY);
                ImageChunk.ImageChunkColor? colorFromSamples = ToColorEnum(c);
                if (iColor != null && colorFromSamples != iColor)
                {
                    return null;
                }
                iColor = colorFromSamples;
            }
            return iColor;
        }
    }
}
