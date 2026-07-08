using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace POC13_NavigationApp.Services
{
    public class NavigationService :
        INavigationService
    {
        private object _currentViewModel;

        public object CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }

            private set
            {
                _currentViewModel = value;

                OnPropertyChanged();
            }
        }

        public string CurrentPageName
        {
            get;
            private set;
        }

        public void Navigate(object viewModel)
        {
            CurrentViewModel = viewModel;

            CurrentPageName = viewModel.GetType()
                              .Name
                              .Replace("ViewModel", "");

            OnPropertyChanged(nameof(CurrentViewModel));

            SaveLastPage();
        }

        private void SaveLastPage()
        {
            if (!Properties.Settings.Default.RememberLastPage)
                return;

            Properties.Settings.Default.LastPage =
                CurrentPageName;

            Properties.Settings.Default.Save();
        }

        public event PropertyChangedEventHandler
            PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName]
            string propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(
                    propertyName));
        }
    }
}