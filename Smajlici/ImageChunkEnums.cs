using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smajlici
{
    struct ImageChunkType
    {
        public ImageChunkColor Color { get; private set; }
        public ImageChunkFace Face { get; private set; }
        private ImageChunk Neighbour;

        public ImageChunkType(ImageChunkColor color, ImageChunkFace face)
        {
            Color = color;
            Face = face;
            Neighbour = null;
        }

        public void SetNeighbour(ImageChunkSide side, ImageChunk chunk)
        {
            Neighbour = chunk;
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
        Blue,
        Black
    }

    public enum ImageChunkRotation
    {
        D0,
        D90,
        D180,
        D270
    }
}
