using System.Windows;
using Smajlici.ViewModel;

namespace Smajlici
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel vm;

        public MainWindow()
        {
           
            InitializeComponent();
            vm = new MainWindowViewModel();
            DataContext = vm;
        }

        
    }
}
