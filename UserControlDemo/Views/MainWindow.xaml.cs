using System.Windows;
using UserControlDemo.ViewModels;

namespace UserControlDemo.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new EmployeeViewModel();
        }
    }
}