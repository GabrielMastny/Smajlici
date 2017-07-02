using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smajlici
{
    

    public enum ImagePartSide
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
