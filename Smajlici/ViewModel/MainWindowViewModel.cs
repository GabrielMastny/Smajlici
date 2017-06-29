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

namespace Smajlici.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        

        public MainWindowViewModel()
        {
            
        }

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


    }
#endregion

}
