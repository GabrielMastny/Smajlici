using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smajlici
{
    class ImageChunk
    {
        public ImageChunkColor Color { get; private set; }
        public ImageChunkFace Face { get; private set; }
        public ImagePart Neighbour { get; private set; }

        public ImageChunk(ImageChunkColor color, ImageChunkFace face)
        {
            Color = color;
            Face = face;
            Neighbour = null;
        }

        public void SetNeighbour(ImagePart imagePart)
        {
            Neighbour = imagePart;
        }

        public enum ImageChunkFace
        {
            Eyes,
            Smile
        }

        public enum ImageChunkColor
        {
            Green,
            Red,
            Yellow,
            Blue,
            Black
        }
    }
}
