using System;
using System.Threading;

namespace Smajlici
{
    class ExperimentalImageSolver :ImageSolver, IImageSolver
    {
        private readonly Uri _uriToImage;
        private readonly Thread[] _threadArray;
        private readonly MyRef<bool> _solved = new MyRef<bool>() {Value=false};
        private readonly MyRef<SplittedImage> _result = new MyRef<SplittedImage>();
        private readonly int[] _defaultImagePosition;
        private readonly int _numberOfThreads;
        private readonly object _mutex;
        public ExperimentalImageSolver(Uri uriToImage,bool cheater)
        {
            _uriToImage = uriToImage;
            _result.Value = new SplittedImage(uriToImage, false);
            _threadArray = new Thread[_numberOfThreads];
            _numberOfThreads = Environment.ProcessorCount;
            _mutex = new object();
            if (cheater)
            {
                _defaultImagePosition = new int[9] { 0, 3, 8, 6, 1, 2, 5, 4, 7 };
            }
            else
            {
                _defaultImagePosition = new int[9] { 0,1,2,3,4,5,6,7,8 };
            }
        }

        

        public new SplittedImage Solve()
        {
            PrepareThreads();
            return _result.Value;
        }

        private void PrepareThreads()
        {
            Console.WriteLine($"Start {DateTime.Now}");
            DateTime date = DateTime.Now;
            object[] parametres = new object[7];
            
            

            parametres[2] = _numberOfThreads;
            parametres[3] = _mutex;
            parametres[4] = _solved;
            parametres[5] = _result;
            int move = 0;
            for (int i = 0; i < _numberOfThreads; i++)
            {
                parametres[1] = _defaultImagePosition;
                SplittedImage tmpSplittedImage = new SplittedImage(_uriToImage, false);
                parametres[0] = tmpSplittedImage;
                parametres[6] = move;
                _threadArray[i] = new Thread(ThreadJob) { Name = $"Smajlici_Thread_#{i}" };
                _threadArray[i].Start((object)parametres);
                NextPermutation(_defaultImagePosition);
                move++;
            }



            foreach (var threads in _threadArray)
            {
                threads.Join();
            }
            TimeSpan duration = DateTime.Now - date;
            Console.WriteLine($"Threads Ended {DateTime.Now} in {move} moves, duration: {duration}");

        }

        static void ThreadJob(object param)
        {
            object[] paramArray = (object[]) param;
            SplittedImage splittedImage = (SplittedImage)paramArray[0];
            int[] nextImagePosition = (int[]) paramArray[1];
            int skips = (int) paramArray[2];
            object zamek = paramArray[3];
            MyRef<bool> solved = (MyRef<bool>)paramArray[4];
            MyRef<SplittedImage> result = (MyRef<SplittedImage>)paramArray[5];
            int move = (int) paramArray[6];
            int modulator = 0;
            int[] internalnextImagePosition;
            lock (zamek)
            {
                 internalnextImagePosition = nextImagePosition;
            }
            



            int[] actualImagePositions = new int[9];
            void GetActualImagePosition(SplittedImage splittedImag, int[] imagePositionArray)
            {
                for (int a = 0; a < 9; a++)
                {
                    imagePositionArray[a] = splittedImag.GetImagePart((SplittedImage.ImagePosittion)a).Id;
                }
            }
            do
            {

                
                    lock (zamek)
                    {

                    move++;
                    if (splittedImage.CheckImageCorectness() && solved.Value == false)
                        {

                            solved.Value = true;
                            GetActualImagePosition(result.Value, actualImagePositions);
                            GetActualImagePosition(splittedImage, internalnextImagePosition );
                            for (int a = 0; a < 9; a++)
                            {

                                if (actualImagePositions[a] != internalnextImagePosition[a])
                                {
                                    int index;
                                    for (index = 0; index < 9; index++)
                                    {
                                        if (internalnextImagePosition[a] == actualImagePositions[index]) break;
                                    }
                                    result.Value.MoveImagePart((SplittedImage.ImagePosittion)index,
                                        (SplittedImage.ImagePosittion)a);
                                    GetActualImagePosition(result.Value, actualImagePositions);
                                }
                                result.Value.GetImagePart((SplittedImage.ImagePosittion) a).Rotation = splittedImage
                                    .GetImagePart((SplittedImage.ImagePosittion) a)
                                    .Rotation;
                                if (result.Value.CheckImageCorectness()) break;
                            }
                        break;
                        }
                        else if (solved.Value == true)
                        {
                        break;
                        }
                    }
                    
                
                modulator++;
                
                if (modulator %10 == 0)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} -- {move}");
                }

                GetActualImagePosition(splittedImage, actualImagePositions);
                for (int a = 0; a < 9; a++)
                {

                    if (actualImagePositions[a] != internalnextImagePosition[a])
                    {
                        int index;
                        for (index = 0; index < 9; index++)
                        {
                            if (internalnextImagePosition[a] == actualImagePositions[index]) break;
                        }
                        splittedImage.MoveImagePart((SplittedImage.ImagePosittion)index,
                            (SplittedImage.ImagePosittion)a);
                        GetActualImagePosition(splittedImage, actualImagePositions);
                    }

                    if (splittedImage.CheckImageCorectness()) break;
                }
                do
                {
                    if (splittedImage.CheckImageCorectness()) break;
                } while (NextRotate(splittedImage));
                
               
            } while (NextPermutation(internalnextImagePosition));

            
        }


        
    }
}
