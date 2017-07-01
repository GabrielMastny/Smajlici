using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Smajlici
{
    class ImagePart
    {
        public int Id { private set; get; }
        public ImageChunkRotation Rotation;
        public BitmapImage ChunkImage { private set; get; }
        public ImageChunkType[] ImageChunkTypes;

        public ImagePart(int id, BitmapImage image,ImageChunkType[] types)
        {
            Id = id;
            Rotation = ImageChunkRotation.D0;
            ChunkImage = image;
            ImageChunkTypes = types;
        }

       

    }

   
}
