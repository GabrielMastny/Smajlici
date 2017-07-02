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

        public SplittedImage(Uri image,bool isDefault)
        {
            _imageParts = ImageSplitter.SplitImage(image,isDefault);
            if (!isDefault)
            {
                foreach (var position in _imageParts)
                {
                    SetNeighbours((ImagePosittion)position.Id);
                }
            }
            


        }

        public BitmapImage GetImageChunkBMI(ImagePosittion pos)
        {
            return _imageParts[((int)pos)].ChunkImage;
        }

        public ImagePart GetImagePart(ImagePosittion position)
        {
            return _imageParts[(int) position];
        }

        private void SetNeighbours(ImagePosittion position)
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
                _imageParts[(int)position].GetImageChunkType((ImagePartSide)i).SetNeighbour(neighbours[i]);
            }

        }

        private ImagePart[] GetNeighbours(ImagePosittion position)
        {
            ImagePart[] neighbours = new ImagePart[4];
            for (int i = 0; i < 4; i++)
            {
                neighbours[i] = _imageParts[(int) position].GetImageChunkType((ImagePartSide) i).Neighbour;
            }
            return neighbours;
        }

        public void RotateImagePart(ImagePosittion position)
        {
            ImagePart imagePart = _imageParts[(int) position];
            ImagePart[] neighbours = GetNeighbours(position);

            for (int i = 3; i < 0; i--)
            {
                _imageParts[(int) position].GetImageChunkType((ImagePartSide) i).SetNeighbour(neighbours[i - 1]);
            }
            _imageParts[(int)position].GetImageChunkType((ImagePartSide)0).SetNeighbour(neighbours[3]);

            if (imagePart.Rotation == ImageChunkRotation.D270)
            {
                imagePart.Rotation = ImageChunkRotation.D0;
            }
            else imagePart.Rotation++;
        }

        public void MoveImagePart(ImagePosittion from, ImagePosittion to)
        {
            ImagePart[] firstImageNeighbours = GetNeighbours(from);
            ImagePart[] secondImageNeighbours = GetNeighbours(to);
            ImagePart firstImagePart = _imageParts[(int) from];
            ImagePart secondImagePart = _imageParts[(int) to];

            void NotifySurroundingNeighbours(ImagePart[] surroundingNeighbours, ImagePart newNeighbour)
            {
                ImagePartSide[] opositeOrder =
                    {ImagePartSide.Right, ImagePartSide.Bottom, ImagePartSide.Left, ImagePartSide.Top};

                for (int i = 0; i < 4; i++)
                {
                    if (surroundingNeighbours[i] != null)
                    {
                        surroundingNeighbours[i].GetImageChunkType(opositeOrder[i]).SetNeighbour(newNeighbour);
                    }
                }
            }

            NotifySurroundingNeighbours(firstImageNeighbours, secondImagePart);
            NotifySurroundingNeighbours(secondImageNeighbours, firstImagePart);

            _imageParts[(int) from] = secondImagePart;
            _imageParts[(int) to] = firstImagePart;

            SetNeighbours(from);
            SetNeighbours(to);
        }
    }
}
