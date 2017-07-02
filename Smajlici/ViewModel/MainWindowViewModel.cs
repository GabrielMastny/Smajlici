using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Smajlici.Commands;
using System.Windows.Media.Imaging;

namespace Smajlici.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        
        
        public MainWindowViewModel()
        {
            LoadImage((new Uri("pack://application:,,,/Img/noImage.PNG")),true);
        }

        public bool ImageLoaded { get; set; } = false;

        private SplittedImage _imageParts;

        public SplittedImage ImageParts
        {
            get => _imageParts;
            set
            {
                _imageParts = value;
                OnPropertyChanged("ImageParts");
                LTImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.LeftTop);
                TImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.Top);
                RTImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.RightTop);
                LMImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.LeftMiddle);
                MImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.Middle);
                RMImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.RightMiddle);
                LBImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.LeftBottom);
                BImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.Bottom);
                RBImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.RightBottom);
            }
        }

        #region Image mesh
        #region 1st row
        private BitmapImage _lMImageSource;
        public BitmapImage LMImageSource
        {
            get => _lMImageSource ?? (_lMImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.LeftMiddle));
            set
            {
                _lMImageSource = value;
                OnPropertyChanged("LMImageSource");
            }
        }

        private BitmapImage _mImageSource;
        public BitmapImage MImageSource
        {
            get => _mImageSource ?? (_mImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.Middle));
            set
            {
                _mImageSource = value;
                OnPropertyChanged("MImageSource");
            }
        }

        private BitmapImage _rMImageSource;
        public BitmapImage RMImageSource
        {
            get => _rMImageSource ?? (_rMImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.RightMiddle));
            set
            {
                _rMImageSource = value;
                OnPropertyChanged("RMImageSource");
            }
        }

        #endregion

        #region 2nd row
        private BitmapImage _lTImageSource;
        public BitmapImage LTImageSource
        {
            get => _lTImageSource ?? (_lTImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.LeftTop));
            set
            {
                _lTImageSource = value;
                OnPropertyChanged("LTImageSource");
            }
        }

        private BitmapImage _tImageSource;
        public BitmapImage TImageSource
        {
            get => _tImageSource ?? (_tImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.Top));
            set
            {
                _tImageSource = value;
                OnPropertyChanged("TImageSource");
            }
        }

        private BitmapImage _rTImageSource;
        public BitmapImage RTImageSource
        {
            get => _rTImageSource ?? (_rTImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.RightTop));
            set
            {
                _rTImageSource = value;
                OnPropertyChanged("RTImageSource");
            }
        }
        #endregion

        #region 3rd row
        private BitmapImage _lBImageSource;
        public BitmapImage LBImageSource
        {
            get => _lBImageSource ?? (_lBImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.LeftBottom));
            set
            {
                _lBImageSource = value;
                OnPropertyChanged("LBImageSource");
            }
        }

        private BitmapImage _bImageSource;
        public BitmapImage BImageSource
        {
            get => _bImageSource ?? (_bImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.Bottom));
            set
            {
                _bImageSource = value;
                OnPropertyChanged("BImageSource");
            }
        }

        private BitmapImage _rBImageSource;
        public BitmapImage RBImageSource
        {
            get => _rBImageSource ?? (_rBImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.RightBottom));
            set
            {
                _rBImageSource = value;
                OnPropertyChanged("RBImageSource");
            }
        }
        #endregion

        #endregion

        public void LoadImage(Uri imageUri,bool isDefault)
        {
            ImageParts = new SplittedImage(imageUri,isDefault);
            if (!isDefault)
            {
                ImageLoaded = true;
            }
        }

        public void SolveImage()
        {
            _imageParts.RotateImagePart(SplittedImage.ImagePosittion.LeftBottom);
            Console.WriteLine($"{_imageParts.GetImagePart(SplittedImage.ImagePosittion.LeftBottom).Rotation}");
            _imageParts.MoveImagePart(SplittedImage.ImagePosittion.LeftTop, SplittedImage.ImagePosittion.RightBottom);
            LTImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.LeftTop);
            RBImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.RightBottom);
        }


        #region Commands
        private ICommand _cmdLoadImage;
        public ICommand CmdLoadImage
        {
            get => _cmdLoadImage ?? (_cmdLoadImage = new ButtonLoadImageClick());
            set => _cmdLoadImage = value;
        }

        private ICommand _cmdSolve;
        public ICommand CmdSolve
        {
            get => _cmdSolve ?? (_cmdSolve = new ButtonSolveClick());
            set => _cmdSolve = value;
        }
#endregion
        #region InotifyPropertyChanged Impl.
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }


}
