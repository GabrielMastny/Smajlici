using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Smajlici
{
    class SplittedImage : ICloneable
    {
        private ImagePart[] _imageParts;

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

        public SplittedImage(Uri image, bool isDefault)
        {
            _imageParts = ImageSplitter.SplitImage(image, isDefault);
            if (!isDefault)
            {
                foreach (var position in _imageParts)
                {
                    InitialSetNeighbours((ImagePosittion)position.Id);
                }
            }



        }

        public BitmapImage GetImageChunkBMI(ImagePosittion pos)
        {
            return _imageParts[((int)pos)].ChunkImage;
        }

        public ImagePart GetImagePart(ImagePosittion position)
        {
            return _imageParts[(int)position];
        }

        private void InitialSetNeighbours(ImagePosittion position)
        {
            int index = 0;
            ImagePart[] neighbours = new ImagePart[4];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    if (index == (int)position)
                    {
                        neighbours[0] = (j == 0) ? null : _imageParts[(int)position - 1];
                        neighbours[1] = (i == 0) ? null : _imageParts[(int)position - 3];
                        neighbours[2] = (j == 2) ? null : _imageParts[(int)position + 1];
                        neighbours[3] = (i == 2) ? null : _imageParts[(int)position + 3];
                    }
                    index++;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                _imageParts[(int)position].GetImageChunk((ImagePartSide)i).SetNeighbour(neighbours[i]);
            }

        }

        private ImagePart[] GetNeighbours(ImagePosittion position)
        {
            ImagePart[] neighbours = new ImagePart[4];
            for (int i = 0; i < 4; i++)
            {
                neighbours[i] = _imageParts[(int)position].GetImageChunk((ImagePartSide)i).Neighbour;
            }
            return neighbours;
        }

        public void RotateImagePart(ImagePosittion position)
        {
            ImagePart imagePart = _imageParts[(int)position];
            ImagePart[] neighbours = GetNeighbours(position);

            imagePart.RotateChunks();

            for (int i = 0; i < 4; i++)
            {
                imagePart.GetImageChunk((ImagePartSide)i).SetNeighbour(neighbours[i]);
            }

            switch (imagePart.Rotation)
            {
                case ImagePartRotation.D0: imagePart.Rotation = ImagePartRotation.D90;
                    break;
                case ImagePartRotation.D90:
                    imagePart.Rotation = ImagePartRotation.D180;
                    break;
                case ImagePartRotation.D180:
                    imagePart.Rotation = ImagePartRotation.D270;
                    break;
                case ImagePartRotation.D270:
                    imagePart.Rotation = ImagePartRotation.D0;
                    break;
            }
        }

        public void MoveImagePart(ImagePosittion from, ImagePosittion to)
        {
            ImagePart[] firstImageNeighbours = GetNeighbours(from);
            ImagePart[] secondImageNeighbours = GetNeighbours(to);
            ImagePart firstImagePart = _imageParts[(int)from];
            ImagePart secondImagePart = _imageParts[(int)to];

            void NotifySurroundingNeighbours(ImagePart[] surroundingNeighbours, ImagePart newNeighbour)
            {
                ImagePartSide[] opositeOrder =
                    {ImagePartSide.Right, ImagePartSide.Bottom, ImagePartSide.Left, ImagePartSide.Top};

                for (int i = 0; i < 4; i++)
                {
                    if (surroundingNeighbours[i] != null)
                    {
                        surroundingNeighbours[i].GetImageChunk(ReversedValue((ImagePartSide)i)).SetNeighbour(newNeighbour);
                    }
                }
            }

            NotifySurroundingNeighbours(firstImageNeighbours, secondImagePart);
            NotifySurroundingNeighbours(secondImageNeighbours, firstImagePart);

            _imageParts[(int)from] = secondImagePart;
            _imageParts[(int)to] = firstImagePart;

            InitialSetNeighbours(from);
            InitialSetNeighbours(to);
        }

        public bool CheckImageCorectness()
        {
            bool result = true;
            foreach (var imagePart in _imageParts)
            {
                for (int i = 0; i < 4; i++)
                {
                    ImageChunk first = imagePart.GetImageChunk((ImagePartSide)i);
                    if (first.Neighbour != null)
                    {
                        ImageChunk second = first.Neighbour.GetImageChunk(ReversedValue((ImagePartSide)i));

                        result = (first.Color == second.Color && first.Face != second.Face);
                        if (!result)
                        {
                            return false;
                        }
                    }

                }
            }
            return true;
        }

        private ImagePartSide ReversedValue(ImagePartSide partSide)
        {
            ImagePartSide result;
            switch (partSide)
            {
                case ImagePartSide.Left:
                    result = ImagePartSide.Right;
                    break;
                case ImagePartSide.Top:
                    result = ImagePartSide.Bottom;
                    break;
                case ImagePartSide.Right:
                    result = ImagePartSide.Left;
                    break;
                case ImagePartSide.Bottom:
                    result = ImagePartSide.Top;
                    break;
                default:
                    result = ImagePartSide.Top;
                    break;
            }
            return result;
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}