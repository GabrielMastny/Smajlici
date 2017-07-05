using System.Windows.Media.Imaging;

namespace Smajlici
{
    class ImagePart
    {
        /// <summary>
        /// Defines former position of ImagePart
        /// </summary>
        public int Id { private set; get; }
        /// <summary>
        /// metadata defining rotation of ImagePart
        /// </summary>
        public ImagePartRotation Rotation;
        /// <summary>
        /// stores Image
        /// </summary>
        public BitmapImage ChunkImage { private set; get; }
        /// <summary>
        /// array of 4 imageChunks representating what is on image
        /// </summary>
        private readonly ImageChunk[] _imageChunkTypes;
        /// <summary>
        /// Creates representation of Part of given Image
        /// </summary>
        /// <param name="id">identification of ImagePart, defines initial position in whole Image, serves as metadata</param>
        /// <param name="image">stores BitmapImage of image part</param>
        /// <param name="imageChunks">array of 4 imageChunks representating what is on image, serves as metadata</param>
        public ImagePart(int id, BitmapImage image, ImageChunk[] imageChunks)
        {
            Id = id;
            Rotation = ImagePartRotation.D0;
            ChunkImage = image;
            _imageChunkTypes = imageChunks;
        }

        /// <summary>
        /// returns one of 4 ImageChunks
        /// </summary>
        /// <param name="imagePartSide">defines which side of ImagePart has to be returned</param>
        /// <returns>ImageChunk</returns>
        public ImageChunk GetImageChunk(ImagePartSide imagePartSide)
        {
            return _imageChunkTypes[(int)imagePartSide];
        }

        /// <summary>
        /// Rotate ImageChunks stored in ImagePart
        /// </summary>
        public void RotateChunks()
        {
            ImageChunk tmp = _imageChunkTypes[3];
            for (int i = 3; i > 0; i--)
            {
                _imageChunkTypes[i] = _imageChunkTypes[i - 1];
            }
            _imageChunkTypes[0] = tmp;
        }

    }
    /// <summary>
    /// represents side of ImagePart
    /// </summary>
    public enum ImagePartSide
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
    }
    /// <summary>
    /// Represents rotation of ImagePart
    /// </summary>
    public enum ImagePartRotation
    {
        D0 = 0,
        D90 = 90,
        D180 = 180,
        D270 = 270
    }


}