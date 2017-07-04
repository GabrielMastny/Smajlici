using System;
using System.Windows.Input;
using Smajlici.ViewModel;

namespace Smajlici.Commands
{
    class ImageClick : ICommand
    {
        public event EventHandler CanExecuteChanged;
        

        public bool CanExecute(object parameter)
        {
            object[] parameterArray = ((object[])parameter);
            return ((MainWindowViewModel)parameterArray[0]).ImageLoaded;
        }

        public void Execute(object parameter)
        {
            object[] parameterArray = ((object[])parameter);
            ((MainWindowViewModel)parameterArray[0]).MoveImage(parameterArray[1].ToString());
            
        }
    }
}
