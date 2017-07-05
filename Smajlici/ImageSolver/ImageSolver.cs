using System;

namespace Smajlici
{
    class ImageSolver : IImageSolver
    {
        /// <summary>
        /// Stores SplittedImage
        /// </summary>
        private readonly SplittedImage _splittedImage;

        private int[] nextImagePosition;
        /// <summary>
        /// Creates ImageSolver
        /// </summary>
        /// <param name="splittedImage">SplittedImage which is used to rearange its actual ImagePart layout</param>
        public ImageSolver(SplittedImage splittedImage,bool cheater)
        {
            _splittedImage = splittedImage;
            if (cheater)
            {
              nextImagePosition = new int[9] { 0, 3, 8, 6, 1, 2, 5, 4, 7 };
            }
            nextImagePosition = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        protected ImageSolver()
        { }
        /// <summary>
        /// Solves Image using BruteForce
        /// </summary>
        public SplittedImage Solve()
        {
            Console.WriteLine($"--Start {DateTime.Now}");
            
            int move = 0;
            do
            {
                if (_splittedImage.CheckImageCorectness()) break;
                int[] actualImagePositions = new int[9];
                GetActualImagePosition();

                void GetActualImagePosition()
                {
                    for (int a = 0; a < 9; a++)
                    {
                        actualImagePositions[a] = _splittedImage.GetImagePart((SplittedImage.ImagePosittion)a).Id;
                    }
                }
                

                for (int a = 0; a < 9; a++)
                {

                    if (actualImagePositions[a] != nextImagePosition[a])
                    {
                        int index;
                        for (index = 0; index < 9; index++)
                        {
                            if (nextImagePosition[a] == actualImagePositions[index]) break;
                        }
                        _splittedImage.MoveImagePart((SplittedImage.ImagePosittion)index,
                            (SplittedImage.ImagePosittion)a);
                        GetActualImagePosition();
                    }


                }
                move++;
                do
                {
                    if (_splittedImage.CheckImageCorectness()) break;
                } while (NextRotate(_splittedImage));
            } while (NextPermutation(nextImagePosition));
            Console.WriteLine($"--End {DateTime.Now}");
            return _splittedImage;
        }

        protected static bool NextRotate(SplittedImage _splittedImage)
        {
            bool existsNextState = true;
            for (int i = 0; i < 9; i++)
            {
                ImagePart part = _splittedImage.GetImagePart((SplittedImage.ImagePosittion) i);
                if (i == 8 && part.Rotation == ImagePartRotation.D270) existsNextState = false;
                if (part.Rotation == ImagePartRotation.D270 && i != 8) continue;
                for (int j = i; j >= 0; j--)
                {
                    _splittedImage.RotateImagePart((SplittedImage.ImagePosittion)j);
                }
                break;
            }
            
            return existsNextState;
        }

        protected static bool NextPermutation(int[] array)
        {
            /* 
             * Next lexicographical permutation algorithm (C#)
             * by Project Nayuki, 2016. Public domain.
             * https://www.nayuki.io/page/next-lexicographical-permutation-algorithm
             */
            int i = array.Length - 1;
            while (i > 0 && array[i - 1] >= array[i])
                i--;
            if (i <= 0)
                return false;

            // Find successor to pivot
            int j = array.Length - 1;
            while (array[j] <= array[i - 1])
                j--;
            int temp = array[i - 1];
            array[i - 1] = array[j];
            array[j] = temp;

            // Reverse suffix
            j = array.Length - 1;
            while (i < j)
            {
                temp = array[i];
                array[i] = array[j];
                array[j] = temp;
                i++;
                j--;
            }
            return true;
        }



    }
}
