using System.ComponentModel;

namespace POC13_NavigationApp.Services
{
    public interface INavigationService
        : INotifyPropertyChanged
    {
        object CurrentViewModel
        {
            get;
        }

        void Navigate(object viewModel);
    }
}