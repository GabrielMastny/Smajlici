using System;
using System.Windows.Input;
using Smajlici.ViewModel;

namespace Smajlici.Commands
{
    class ToggleButtonClick : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (((MainWindowViewModel) parameter).ToggleButtonContent == "Brute Force")
            {
                ((MainWindowViewModel) parameter).ToggleButtonContent = "Multi threading";
            }
            else
            {
                ((MainWindowViewModel) parameter).ToggleButtonContent = "Brute Force";
            }
            
        }

        public event EventHandler CanExecuteChanged;
    }
}
