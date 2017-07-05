﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Smajlici.ViewModel;

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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Vše podporované (*.png, *jpg)|*.png;*.jpg|PNG (*.png)|*.png|JPG (*.jpg)|*.jpg";
            openFileDialog.FileOk += OpenFileDialog_FileOk;
            if (openFileDialog.ShowDialog() == true)
            {
                var imgUri = new Uri(openFileDialog.FileName);
                try
                {
                    ((MainWindowViewModel)parameter).LoadImage(imgUri, false);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                
            }

        }
        
        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            BitmapDecoder decoder = BitmapDecoder.Create(new Uri( ((OpenFileDialog)sender).FileName ), BitmapCreateOptions.None, BitmapCacheOption.None);
            BitmapFrame frame = decoder.Frames[0];
            if ((int)frame.Width != (int)frame.Height)
            {
                e.Cancel = true;
                MessageBox.Show("Obrázek musí mít stejnou šířku a výšku.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else if (frame.Width < 400)
            {
                e.Cancel = true;
                MessageBox.Show("Obrázek je příliš malý.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            

        }
        #endregion
    }
}
