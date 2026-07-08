using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace POC13_NavigationApp.Services
{
    public class ApplicationStateService : INotifyPropertyChanged
    {
        private readonly StartupService _startupService;

        private bool _showNavigationMenu =
            Properties.Settings.Default.ShowNavigationMenu;

        public bool ShowNavigationMenu
        {
            get
            {
                return _showNavigationMenu;
            }

            set
            {
                if (_showNavigationMenu != value)
                {
                    _showNavigationMenu = value;

                    Properties.Settings.Default.ShowNavigationMenu = value;
                    Properties.Settings.Default.Save();

                    OnPropertyChanged();
                }
            }
        }

        private bool _startMaximized =
    Properties.Settings.Default.StartMaximized;

        public bool StartMaximized
        {
            get
            {
                return _startMaximized;
            }

            set
            {
                if (_startMaximized != value)
                {
                    _startMaximized = value;

                    Properties.Settings.Default.StartMaximized = value;

                    Properties.Settings.Default.Save();

                    OnPropertyChanged();
                }
            }
        }

        private bool _rememberLastPage =
    Properties.Settings.Default.RememberLastPage;

        public bool RememberLastPage
        {
            get
            {
                return _rememberLastPage;
            }

            set
            {
                if (_rememberLastPage != value)
                {
                    _rememberLastPage = value;

                    Properties.Settings.Default.RememberLastPage = value;

                    Properties.Settings.Default.Save();

                    OnPropertyChanged();
                }
            }
        }

        private bool _startWithWindows;
        public bool StartWithWindows
        {
            get
            {
                return _startWithWindows;
            }

            set
            {
                if (_startWithWindows != value)
                {
                    _startWithWindows = value;

                    _startupService.SetStartup(value);

                    Properties.Settings.Default.StartWithWindows = value;

                    Properties.Settings.Default.Save();

                    OnPropertyChanged();
                }
            }
        }

        public ApplicationStateService()
        {
            _startupService = new StartupService();
            _startWithWindows =  _startupService.IsStartupEnabled();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}