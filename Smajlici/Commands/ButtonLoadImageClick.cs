using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Smajlici.Commands
{
    class ButtonLoadImageClick : ICommand
    {
        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter)
        {
            Uri imgUri;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg";
            openFileDialog.FileOk += OpenFileDialog_FileOk;
            if (openFileDialog.ShowDialog() == true)
            {
                imgUri =new Uri(openFileDialog.FileName);
                ((Smajlici.ViewModel.MainWindowViewModel) parameter).LoadImage(imgUri);
            }

        }

        private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            
        }
        #endregion
    }
}
