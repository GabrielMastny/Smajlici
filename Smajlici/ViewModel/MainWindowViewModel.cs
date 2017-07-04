using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Smajlici.Commands;
using System.Windows.Media.Imaging;

namespace Smajlici.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor, Loads default image
        /// </summary>
        public MainWindowViewModel()
        {
            LoadImage((new Uri("pack://application:,,,/Img/noImage.PNG")),true);
        }

        /// <summary>
        /// Represents if is in application loaded default image or users;
        /// </summary>
        public bool ImageLoaded { get; private set; }
        /// <summary>
        /// stores splitted version of users image
        /// </summary>
        private SplittedImage _imageParts;
        /// <summary>
        /// stores splitted version of users image
        /// </summary>
        public SplittedImage ImageParts
        {
            get => _imageParts;
            set
            {
                _imageParts = value;
                OnPropertyChanged();
                RefreshImage();
                
            }
        }

        #region Image mesh
       

        #region 1st row
        /// <summary>
        /// Image of Left Top imagePart
        /// </summary>
        private BitmapImage _lTImageSource;
        /// <summary>
        /// Image of Left Top imagePart
        /// </summary>
        public BitmapImage LTImageSource
        {
            get => _lTImageSource ;
            set
            {
                _lTImageSource = value;
                OnPropertyChanged("LTImageSource");
            }
        }
        /// <summary>
        /// Rotation of Left Top ImagePart
        /// </summary>
        private double _lTImageAngle;
        /// <summary>
        /// Rotation of Left Top ImagePart
        /// </summary>
        public double LTImageAngle
        {
            get => _lTImageAngle;
            set
            {
                _lTImageAngle = value;
                OnPropertyChanged("LTImageAngle");
            }
        }
        /// <summary>
        /// Image of  Top imagePart
        /// </summary>
        private BitmapImage _tImageSource;
        /// <summary>
        /// Image of  Top imagePart
        /// </summary>
        public BitmapImage TImageSource
        {
            get => _tImageSource;
            set
            {
                _tImageSource = value;
                OnPropertyChanged("TImageSource");
            }
        }
        /// <summary>
        /// Rotation of Top ImagePart
        /// </summary>
        private double _tImageAngle;
        /// <summary>
        /// Rotation of  Top ImagePart
        /// </summary>
        public double TImageAngle
        {
            get => _tImageAngle;
            set
            {
                _tImageAngle = value;
                OnPropertyChanged("TImageAngle");
            }
        }
        /// <summary>
        /// Image of Right Top imagePart
        /// </summary>
        private BitmapImage _rTImageSource;
        /// <summary>
        /// Image of Right Top imagePart
        /// </summary>
        public BitmapImage RTImageSource
        {
            get => _rTImageSource ;
            set
            {
                _rTImageSource = value;
                OnPropertyChanged("RTImageSource");
            }
        }
        /// <summary>
        /// Rotation of Right Top ImagePart
        /// </summary>
        private double _rTImageAngle;
        /// <summary>
        /// Rotation of Right Top ImagePart
        /// </summary>
        public double RTImageAngle
        {
            get => _rTImageAngle;
            set
            {
                _rTImageAngle = value;
                OnPropertyChanged("RTImageAngle");
            }
        }

        #endregion
        #region 2nd row
        /// <summary>
        /// Image of Left Middle imagePart
        /// </summary>
        private BitmapImage _lMImageSource;
        /// <summary>
        /// Image of Left Middle imagePart
        /// </summary>
        public BitmapImage LMImageSource
        {
            get => _lMImageSource;
            set
            {
                _lMImageSource = value;
                OnPropertyChanged("LMImageSource");
            }
        }
        /// <summary>
        /// Rotation of Left Middle ImagePart
        /// </summary>
        private double _lMImageAngle;
        /// <summary>
        /// Rotation of Left Middle ImagePart
        /// </summary>
        public double LMImageAngle
        {
            get => _lMImageAngle;
            set
            {
                _lMImageAngle = value;
                OnPropertyChanged("LMImageAngle");
            }
        }

        /// <summary>
        /// Image of Middle imagePart
        /// </summary>
        private BitmapImage _mImageSource;
        /// <summary>
        /// Image of Middle imagePart
        /// </summary>
        public BitmapImage MImageSource
        {
            get => _mImageSource ;
            set
            {
                _mImageSource = value;
                OnPropertyChanged("MImageSource");
            }
        }
        /// <summary>
        /// Rotation of Middle ImagePart
        /// </summary>
        private double _mImageAngle;
        /// <summary>
        /// Rotation of Left Middle ImagePart
        /// </summary>
        public double MImageAngle
        {
            get =>_mImageAngle;
            set
            {
                _mImageAngle = value;
                OnPropertyChanged("MImageAngle");
            }
        }
        /// <summary>
        /// Image of Right Middle imagePart
        /// </summary>
        private BitmapImage _rMImageSource;
        /// <summary>
        /// Image of Right Middle imagePart
        /// </summary>
        public BitmapImage RMImageSource
        {
            get => _rMImageSource;
            set
            {
                _rMImageSource = value;
                OnPropertyChanged("RMImageSource");
            }
        }
        /// <summary>
        /// Rotation of Right Middle ImagePart
        /// </summary>
        private double _rMImageAngle;
        /// <summary>
        /// Rotation of Right Middle ImagePart
        /// </summary>
        public double RMImageAngle
        {
            get => _rTImageAngle;
            set
            {
                _rMImageAngle = value;
                OnPropertyChanged("RMImageAngle");
            }
        }

        #endregion
        #region 3rd row
        /// <summary>
        /// Image of Left Bottom imagePart
        /// </summary>
        private BitmapImage _lBImageSource;
        /// <summary>
        /// Image of Left Bottom imagePart
        /// </summary>
        public BitmapImage LBImageSource
        {
            get => _lBImageSource ;
            set
            {
                _lBImageSource = value;
                OnPropertyChanged("LBImageSource");
            }
        }
        /// <summary>
        /// Rotation of Left Bottom ImagePart
        /// </summary>
        private double _lBImageAngle;
        /// <summary>
        /// Rotation of Left Bottom ImagePart
        /// </summary>
        public double LBImageAngle
        {
            get =>_lBImageAngle;
            set
            {
                _lBImageAngle = value;
                OnPropertyChanged("LBImageAngle");
            }
        }
        /// <summary>
        /// Image of Bottom imagePart
        /// </summary>
        private BitmapImage _bImageSource;
        /// <summary>
        /// Image of Bottom imagePart
        /// </summary>
        public BitmapImage BImageSource
        {
            get => _bImageSource;
            set
            {
                _bImageSource = value;
                OnPropertyChanged("BImageSource");
            }
        }
        /// <summary>
        /// Rotation of Bottom ImagePart
        /// </summary>
        private double _bImageAngle;
        /// <summary>
        /// Rotation of Bottom ImagePart
        /// </summary>
        public double BImageAngle
        {
            get => _bImageAngle;
            set
            {
                _bImageAngle = value;
                OnPropertyChanged("BImageAngle");
            }
        }
        /// <summary>
        /// Image of Right Bottom imagePart
        /// </summary>
        private BitmapImage _rBImageSource;
        /// <summary>
        /// Image of Right Bottom imagePart
        /// </summary>
        public BitmapImage RBImageSource
        {
            get => _rBImageSource ;
            set
            {
                _rBImageSource = value;
                OnPropertyChanged("RBImageSource");
            }
        }
        /// <summary>
        /// Rotation of Right Bottom ImagePart
        /// </summary>
        private double _rBImageAngle;
        /// <summary>
        /// Rotation of Right Bottom ImagePart
        /// </summary>
        public double RBImageAngle
        {
            get => _rBImageAngle;
            set
            {
                _rBImageAngle = value;
                OnPropertyChanged("RBImageAngle");
            }
        }
        #endregion

        #endregion

        #region CommandFoos
        /// <summary>
        /// Called by Command, Loads new Image to be splitted
        /// </summary>
        /// <param name="imageUri">path to the Image</param>
        /// <param name="isDefault">represents if image is default placeholder or users image, based on thi si decided whether to ImageParts store metadata</param>
        public void LoadImage(Uri imageUri, bool isDefault)
        {
            ImageParts = new SplittedImage(imageUri, isDefault);
            if (!isDefault)
            {
                ImageLoaded = true;
            }
        }
        /// <summary>
        /// Called by Command, solves Image
        /// </summary>
        public void SolveImage()
        {
            ImageSolver imageSolver = new ImageSolver(_imageParts);
            imageSolver.Solve();
            RefreshImage();
        }

        /// <summary>
        /// filled by Command on first click
        /// </summary>
        private SplittedImage.ImagePosittion? _moveImageFrom;
        /// <summary>
        /// filled by Command on second click
        /// </summary>
        private SplittedImage.ImagePosittion? _moveImageTo;
        /// <summary>
        /// if both _moveImagexx ar filled, move ImageParts
        /// </summary>
        /// <param name="elementName">Name of element in View that calls this foo</param>
        public void MoveImage(string elementName)
        {
            PrintImagePartInfo(elementName);
            if (_moveImageFrom == null)//on first time stores to _moveImageFrom
            {
                _moveImageFrom = ElementNameConv(elementName);
            }
            else
            {
                _moveImageTo = ElementNameConv(elementName); // on second time stores to  _moveImageFrom, invokes foo to move ImageParts and empties both properties for next use
                ImageParts.MoveImagePart(_moveImageFrom.Value, _moveImageTo.Value);
                _moveImageFrom = null;
                _moveImageTo = null;
                RefreshImage();
            }  
        }

        private void PrintImagePartInfo(string elementName)
        {
            ImagePart ip = ImageParts.GetImagePart(ElementNameConv(elementName).Value);
            Console.WriteLine($"Image ID: {ip.Id} rot: {ip.Rotation.ToString()}");
            if (ip.GetImageChunk(ImagePartSide.Left).Neighbour == null)
            {
                Console.WriteLine($"left chunkColor: {ip.GetImageChunk(ImagePartSide.Left).Color} chunkFace: {ip.GetImageChunk(ImagePartSide.Left).Face} neighID: NULL");
            }
            else Console.WriteLine($"left chunkColor: {ip.GetImageChunk(ImagePartSide.Left).Color} chunkFace: {ip.GetImageChunk(ImagePartSide.Left).Face} neighID: {ip.GetImageChunk(ImagePartSide.Left).Neighbour.Id}");
            if (ip.GetImageChunk(ImagePartSide.Top).Neighbour == null)
            {
                Console.WriteLine($"top chunkColor: {ip.GetImageChunk(ImagePartSide.Top).Color} chunkFace: {ip.GetImageChunk(ImagePartSide.Top).Face} neighID: NULL");
            }
            else Console.WriteLine($"top chunkColor: {ip.GetImageChunk(ImagePartSide.Top).Color} chunkFace: {ip.GetImageChunk(ImagePartSide.Top).Face} neighID: {ip.GetImageChunk(ImagePartSide.Top).Neighbour.Id}");
            if (ip.GetImageChunk(ImagePartSide.Right).Neighbour == null)
            {
                Console.WriteLine($"right chunkColor: {ip.GetImageChunk(ImagePartSide.Right).Color} chunkFace: {ip.GetImageChunk(ImagePartSide.Right).Face} neighID: NULL");
            }
            else Console.WriteLine($"right chunkColor {ip.GetImageChunk(ImagePartSide.Right).Color} chunkFace: {ip.GetImageChunk(ImagePartSide.Right).Face} neighID: {ip.GetImageChunk(ImagePartSide.Right).Neighbour.Id}");
            if (ip.GetImageChunk(ImagePartSide.Bottom).Neighbour == null)
            {
                Console.WriteLine($"bottom chunkColor {ip.GetImageChunk(ImagePartSide.Bottom).Color} chunkFace: {ip.GetImageChunk(ImagePartSide.Bottom).Face} neighID: NULL");
            }
            else Console.WriteLine($"bottom chunkColor {ip.GetImageChunk(ImagePartSide.Bottom).Color} chunkFace: {ip.GetImageChunk(ImagePartSide.Right).Face} neighID: {ip.GetImageChunk(ImagePartSide.Bottom).Neighbour.Id}");
            Console.WriteLine("\n \n");
        }

        /// <summary>
        /// Called by Command Invokes foo to rotate ImagePart
        /// </summary>
        /// <param name="elementName">Name of element in View that calls this foo</param>
        public void RotateImage(string elementName)
        {
            var elementNameConv = ElementNameConv(elementName);
            if (elementNameConv != null) ImageParts.RotateImagePart(elementNameConv.Value);
            RefreshImage();
            _moveImageTo = null;//on doubleclick is ivoked also simple click, this prevents from bad behaviour
            _moveImageFrom = null;
        }
        /// <summary>
        /// Convert name of element in View to ImagePosition
        /// </summary>
        /// <param name="elementName">Name of element in View that calls this foo</param>
        /// <returns>ImagePosition enum</returns>
        private SplittedImage.ImagePosittion? ElementNameConv(string elementName)
        {
            switch (elementName)
            {
                case "LT":return SplittedImage.ImagePosittion.LeftTop;
                case "T":return SplittedImage.ImagePosittion.Top;
                case "RT":return SplittedImage.ImagePosittion.RightTop;
                case "LM":return SplittedImage.ImagePosittion.LeftMiddle;
                case "M":return SplittedImage.ImagePosittion.Middle;
                case "RM":return SplittedImage.ImagePosittion.RightMiddle;
                case "LB":return SplittedImage.ImagePosittion.LeftBottom;
                case "B":return SplittedImage.ImagePosittion.Bottom;
                case "RB":return SplittedImage.ImagePosittion.RightBottom;
            }
            return null;
        }
        #endregion





        /// <summary>
        /// refreshes whole image
        /// </summary>
        private void RefreshImage()
        {
            LTImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.LeftTop);
            LTImageAngle = (double) ImageParts.GetImagePart(SplittedImage.ImagePosittion.LeftTop).Rotation;

            TImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.Top);
            TImageAngle = (double)ImageParts.GetImagePart(SplittedImage.ImagePosittion.Top).Rotation;

            RTImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.RightTop);
            RTImageAngle = (double)ImageParts.GetImagePart(SplittedImage.ImagePosittion.RightTop).Rotation;

            LMImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.LeftMiddle);
            LMImageAngle = (double)ImageParts.GetImagePart(SplittedImage.ImagePosittion.LeftMiddle).Rotation;

            MImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.Middle);
            MImageAngle = (double)ImageParts.GetImagePart(SplittedImage.ImagePosittion.Middle).Rotation;

            RMImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.RightMiddle);
            RMImageAngle = (double)ImageParts.GetImagePart(SplittedImage.ImagePosittion.RightMiddle).Rotation;

            LBImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.LeftBottom);
            LBImageAngle = (double)ImageParts.GetImagePart(SplittedImage.ImagePosittion.LeftBottom).Rotation;

            BImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.Bottom);
            BImageAngle = (double)ImageParts.GetImagePart(SplittedImage.ImagePosittion.Bottom).Rotation;

            RBImageSource = ImageParts.GetImageChunkBMI(SplittedImage.ImagePosittion.RightBottom);
            RBImageAngle = (double)ImageParts.GetImagePart(SplittedImage.ImagePosittion.RightBottom).Rotation;
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

        private ICommand _cmdImageClick;
        public ICommand CmdImageClick
        {
            get => _cmdImageClick ?? (_cmdImageClick = new ImageClick());
            set => _cmdImageClick = value;
        }

        private ICommand _cmdImageDoubleClick;
        public ICommand CmdImageDoubleClick
        {
            get => _cmdImageDoubleClick ?? (_cmdImageDoubleClick = new ImageDoubleClick());
            set => _cmdImageDoubleClick = value;
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
