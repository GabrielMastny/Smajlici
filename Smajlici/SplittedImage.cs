using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Smajlici
{
    class SplittedImage
    {
        private ImageChunk[] _imageChunks;

        public enum ImagePosittion
        {
            LeftTop = 0,
            Top = 1,
            RightTop = 2,
            LeftMiddle = 3,
            Middle = 4,
            RightMiddle = 5,
            LeftBottom = 6,
            Bottom = 7,
            RightBottom = 8
        }

        public SplittedImage(Uri image)
        {
            _imageChunks = ImageSplitter.SplitImage(image);

        }

        public BitmapImage GetImageChunkBMI(ImagePosittion pos)
        {
            return _imageChunks[((int)pos)].ChunkImage;
        }
    }
}
