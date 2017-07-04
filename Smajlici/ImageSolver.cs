using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smajlici
{
    class ImageSolver
    {
        private SplittedImage splittedImage;
        public ImageSolver(SplittedImage splittedImage)
        {
            this.splittedImage = splittedImage;
        }

        public void Solve()
        {
            Console.WriteLine($"--Start {DateTime.Now}");
            int[] nextImagePosition = new int[9]{0,1,2,3,4,5,6,7,8};
            int move = 0;
            bool correct = false;
            do
            {
                if ((correct = splittedImage.CheckImageCorectness()) == true) break;
                int[] actualImagePositions = new int[9];
                GetActualImagePosition();

                void GetActualImagePosition()
                {
                    for (int a = 0; a < 9; a++)
                    {
                        actualImagePositions[a] = splittedImage.GetImagePart((SplittedImage.ImagePosittion)a).Id;
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
                        splittedImage.MoveImagePart((SplittedImage.ImagePosittion)index,
                            (SplittedImage.ImagePosittion)a);
                        GetActualImagePosition();
                    }


                }
                move++;
                if (move % 10 == 0)
                {
                    Console.WriteLine($"-- {move}");
                }
                do
                {
                    
                    
                    
                    if (splittedImage.CheckImageCorectness()) break;
                    // Console.WriteLine($"xx {move}  {rotate}");
                    int[] sample = new int[9] { 4, 1, 2, 3, 0, 5, 6, 7, 8 };
                    ImagePartRotation[] rotsample = new ImagePartRotation[9]
                    {
                        ImagePartRotation.D90, ImagePartRotation.D0, ImagePartRotation.D0, ImagePartRotation.D0,
                        ImagePartRotation.D270, ImagePartRotation.D0, ImagePartRotation.D0, ImagePartRotation.D0,
                        ImagePartRotation.D0
                    };
                    bool test1 = true;
                    bool test2 = true;
                    for (int i = 0; i < 9; i++)
                    {
                        if (sample[i] != actualImagePositions[i])
                        {
                            test1 = false;
                        }
                        if (rotsample[i] != splittedImage.GetImagePart((SplittedImage.ImagePosittion)i).Rotation)
                        {
                            test2 = false;
                        }
                    }
                    if (test1)
                    {
                        Console.WriteLine();
                        if (test2)
                        {
                            Console.WriteLine("");
                        }
                    }

                    
                    if (test1 && test2)
                    {
                       // && (splittedImage.GetImagePart(SplittedImage.ImagePosittion.Top).Rotation == ImagePartRotation.D90)
                        Console.Write("Bazinga");
                        splittedImage.CheckImageCorectness();
                    }
                } while (Rotate());
            } while (Move(nextImagePosition));
            Console.WriteLine($"--End {DateTime.Now}");


        }

        private bool Rotate()
        {
            bool existsNextState = true;
            for (int i = 0; i < 9; i++)
            {
                ImagePart part = splittedImage.GetImagePart((SplittedImage.ImagePosittion) i);
                if (i == 8 && part.Rotation == ImagePartRotation.D270) existsNextState = false;
                if (part.Rotation == ImagePartRotation.D270 && i != 8) continue;
                for (int j = i; j >= 0; j--)
                {
                    splittedImage.RotateImagePart((SplittedImage.ImagePosittion)j);
                }
                break;
            }
            
            return existsNextState;
        }

        private bool Move(int[] array)
        {
            int i = array.Length - 1;
            while (i > 0 && array[i - 1] >= array[i])
                i--;
            if (i <= 0)
            {
                foreach (var ss in array)
                {
                    Console.WriteLine($"{ss.ToString()}");
                }
                return false;
            }
                


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
