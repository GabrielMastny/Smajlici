using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smajlici
{
    class ImageChunkType
    {
        public ImageChunkColor Color { get; private set; }
        public ImageChunkFace Face { get; private set; }
        public ImagePart Neighbour { get; private set; }

        public ImageChunkType(ImageChunkColor color, ImageChunkFace face)
        {
            Color = color;
            Face = face;
            Neighbour = null;
        }

        public void SetNeighbour(ImagePart imagePart)
        {
            Neighbour = imagePart;
        }
    }
}
