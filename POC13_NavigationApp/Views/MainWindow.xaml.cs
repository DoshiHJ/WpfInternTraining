using System.Windows;
using POC13_NavigationApp.ViewModels;

namespace POC13_NavigationApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Properties.Settings.Default.StartMaximized)
            {
                WindowState = WindowState.Maximized;
            }

            DataContext = new MainViewModel();
        }
    }
}