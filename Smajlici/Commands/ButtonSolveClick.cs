using System;
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
                return (((MainWindowViewModel)parameter).ImageLoaded);
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
            ((MainWindowViewModel) parameter).SolveImage();

        }
        #endregion
    }
}
