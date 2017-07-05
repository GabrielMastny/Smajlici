

namespace Smajlici
{
    class ImageChunk
    {
        /// <summary>
        /// metadata represents what color is on ImageChunk
        /// </summary>
        public ImageChunkColor Color { get; private set; }
        /// <summary>
        /// metadata represents if is on ImageChunk smile or eyes
        /// </summary>
        public ImageChunkFace Face { get; private set; }
        /// <summary>
        /// stores reference to neighbour ImagePart
        /// </summary>
        public ImagePart Neighbour { get; private set; }

        /// <summary>
        /// Creates ImageChunk metadata
        /// </summary>
        /// <param name="color">represent what color is on this ImageChunk</param>
        /// <param name="face">represent what kind of ImageChunkFace is on this ImageChunk</param>
        public ImageChunk(ImageChunkColor color, ImageChunkFace face)
        {
            Color = color;
            Face = face;
            Neighbour = null;
        }
        /// <summary>
        /// Sets new neighbour to ImageChunk, usable for move or rotating
        /// </summary>
        /// <param name="imagePart">reference on ImagePart</param>
        public void SetNeighbour(ImagePart imagePart)
        {
            Neighbour = imagePart;
        }
        /// <summary>
        /// Defines if is on ImageChunk smile or Eye
        /// </summary>
        public enum ImageChunkFace
        {
            Eyes,
            Smile
        }
        /// <summary>
        /// Defines color of ImageChunk
        /// </summary>
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
