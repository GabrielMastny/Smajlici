using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;


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
            else return null;
            

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

        private static ImageChunkType[] PixelRecognize(Bitmap bitmap)
        {
            ImageChunkType[] chunkType = new ImageChunkType[4];
            Point[] colorRelativeSamples = {new Point(29, 49), new Point(2, 74), new Point(2, 24)};
            Point[] eyeRelativeSamples = { new Point(15, 58), new Point(15, 40) };
            Point[] smileRelativeSamples = { new Point(10, 69), new Point(7, 29), new Point(24, 49) };

            for (int i = 0; i < chunkType.Length; i++)
            {
                ImageChunkColor? iColor = null;
                ImageChunkFace? iFace = null;

                iColor = CheckColorFromSamples(colorRelativeSamples, bitmap);
                if (iColor != ImageChunkColor.Black && iColor != null)
                {
                    if (CheckColorFromSamples(smileRelativeSamples,bitmap) == ImageChunkColor.Black)
                    {
                        iFace = ImageChunkFace.Smile;
                    }
                    else if (CheckColorFromSamples(eyeRelativeSamples,bitmap) == ImageChunkColor.Black)
                    {
                        iFace = ImageChunkFace.Eyes;
                    }

                }

                if (iColor != null && iFace != null)
                {
                    chunkType[i] = new ImageChunkType(iColor.Value, iFace.Value);
                    bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else
                {
                    throw new Exception("Nelze rozeznat obrazek.");
                }
                
                
            }
            return chunkType;
        }

        private static ImageChunkColor? ToColorEnum(Color color)
        {
            ImageChunkColor iColor;

            int r = color.R;
            int g = color.G;
            int b = color.B;
            if (r > (g + 50) && (r > b + 50))
            {
                iColor = ImageChunkColor.Red;
            }
            else if (g > (r + 50) && g > (b + 50))
            {
                iColor = ImageChunkColor.Green;
            }
            else if (Math.Abs(g - b) <100 && b - r > 50) 
            {
                iColor = ImageChunkColor.Blue;
            }
            else if (Math.Abs(r - g) < 25 && r > (b + 50))
            {
                iColor = ImageChunkColor.Yellow;
            }
            else if (r < 30 && g < 30 && b < 30)
            {
                iColor = ImageChunkColor.Black;
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

        private static ImageChunkColor? CheckColorFromSamples(Point[] samples,Bitmap bitmap)
        {
            ImageChunkColor? iColor = null;
            

            foreach (Point t in samples)
            {
                int absolutePointX = FromPercentage(t.X, bitmap.Width);
                int absolutePointY = FromPercentage(t.Y, bitmap.Height);
                Color c = bitmap.GetPixel(absolutePointX,absolutePointY);
                ImageChunkColor? colorFromSamples = ToColorEnum(c);
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
