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
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;


namespace Smajlici
{
    static class ImageSplitter
    {

        public static ImageChunk[] SplitImage(Uri wholeImage,bool isDefault)
        {

            Bitmap BM = CreateBM(wholeImage);
            
            int partSize = (BM.Width  / 3);

            if (partSize > 0)
            {
                int index = 0;
                ImageChunk[] splittedImage = new ImageChunk[9];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Bitmap BMChunk = BM.Clone(new Rectangle(j * partSize, i * partSize, partSize, partSize),
                            BM.PixelFormat);
                        if (isDefault)
                        {
                            splittedImage[index] = new ImageChunk(index, ConvToBMI(BMChunk), null);
                        }
                        else splittedImage[index] = new ImageChunk(index, ConvToBMI(BMChunk), PixelRecognize(BMChunk));
                        index++;
                    }
                }
                return splittedImage;
            }
            else return null;
            

        }

        private static BitmapImage ConvToBMI(Bitmap bitmap)
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

        private static ImageChunkType[] PixelRecognize(Bitmap bitmap)
        {
            bool imageRecognized = true;
            
            Point[] colorSamples = {new Point(85, 145), new Point(5, 220), new Point(5, 70)};
            Point[] SmileSamples = { new Point(30, 205), new Point(20, 85), new Point(72, 145) };
            Point[] EyeSamples = {new Point(45, 172), new Point(45, 120)};

            ImageChunkType[] chunkType = new ImageChunkType[4];
            for (int i = 0; i < 4; i++)
            {
                ImageChunkColor? iColor = null;
                ImageChunkFace? iFace = null;
                bool smileRecognized = true;
                bool eyesRecognized = true;
                for (int j = 0; j < chunkType.Length-1; j++)
                {
                    Color c = bitmap.GetPixel(colorSamples[j].X, colorSamples[j].Y);
                    ImageChunkColor? colorFromSamples = GetColor(c);
                    if (iColor == null)
                    {
                       iColor = colorFromSamples;
                    }
                    else if (iColor != colorFromSamples || iColor == ImageChunkColor.Black)
                    {
                        imageRecognized = false;
                        break;
                    }

                }
                //Smile
                for (int j = 0; j < SmileSamples.Length-1; j++)
                {
                    Color c = bitmap.GetPixel(SmileSamples[j].X, SmileSamples[j].Y);
                    ImageChunkColor? colorFromSamples = GetColor(c);
                    if (iFace == null && colorFromSamples == ImageChunkColor.Black)
                    {
                        iFace = ImageChunkFace.Smile;
                    }
                    else if (colorFromSamples != ImageChunkColor.Black)
                    {
                        smileRecognized = false;
                        break;
                    }
                    
                }
                //Eye
                for (int j = 0; j < EyeSamples.Length-1; j++)
                {
                    Color c = bitmap.GetPixel(EyeSamples[j].X, EyeSamples[j].Y);
                    ImageChunkColor? colorFromSamples = GetColor(c);
                    if (iFace == null && colorFromSamples == ImageChunkColor.Black)
                    {
                        iFace = ImageChunkFace.Eyes;
                    }
                    else if (colorFromSamples != ImageChunkColor.Black)
                    {
                        eyesRecognized = false;
                        break;
                    }

                }
                if (!(imageRecognized && smileRecognized ^ eyesRecognized))
                {
                    imageRecognized = false;
                    break;
                }

                if (iColor != null && iFace != null) chunkType[i] = new ImageChunkType((ImageChunkColor) iColor, (ImageChunkFace) iFace);
                bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            return (imageRecognized) ? chunkType : null;
        }

        private static ImageChunkColor? GetColor(Color color)
        {
            ImageChunkColor iColor;

            int R = ((int)color.R);
            int G = ((int)color.G);
            int B = ((int)color.B);
            if (R > (G + 50) && (R > B + 50))
            {
                iColor = ImageChunkColor.Red;
            }
            else if (G > (R + 50) && G > (B + 50))
            {
                iColor = ImageChunkColor.Green;
            }
            else if (Math.Abs(G - B) <100 && B - R > 50) 
            {
                iColor = ImageChunkColor.Blue;
            }
            else if (Math.Abs(R - G) < 25 && R > (B + 50))
            {
                iColor = ImageChunkColor.Yellow;
            }
            else if (R < 30 && G < 30 && B < 30)
            {
                iColor = ImageChunkColor.Black;
            }
            else
            {
                return null;
            }
            return iColor;

        }

        

        
    }
}
