using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Smajlici.ViewModel;

namespace Smajlici.Commands
{
    class ButtonSolveClick : ICommand
    {
        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                return (((MainWindowViewModel)parameter).ImageLoaded) ? true : false;
            }
            return true;

        }
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public void Execute(object parameter)
        {


        }
        #endregion
    }
}
