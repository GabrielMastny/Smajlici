using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smajlici
{
    struct ImageChunkType
    {
        public ImageChunkColor Color;
        public ImageChunkFace Face;
        public ImageChunk Neighbour;

        public ImageChunkType(ImageChunkColor color, ImageChunkFace face, ImageChunk neighbour)
        {
            Color = color;
            Face = face;
            Neighbour = neighbour;
        }
    }

    public enum ImageChunkSide
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
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
        Blue
    }

    public enum ImageChunkRotation
    {
        D0,
        D90,
        D180,
        D270
    }
}
