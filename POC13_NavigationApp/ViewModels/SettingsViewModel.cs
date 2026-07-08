using System.ComponentModel;
using System.Runtime.CompilerServices;

using POC13_NavigationApp.Enums;
using POC13_NavigationApp.Services;

namespace POC13_NavigationApp.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly ThemeService _themeService;

        private readonly SettingsService _settingsService;

        public event PropertyChangedEventHandler PropertyChanged;

        private ThemeType _selectedTheme;

        public ThemeType SelectedTheme
        {
            get
            {
                return _selectedTheme;
            }

            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;

                    OnPropertyChanged();

                    _themeService.ApplyTheme(value);

                    _settingsService.SaveTheme(value);
                }
            }
        }

        public bool ShowNavigationMenu
        {
            get
            {
                return _applicationState.ShowNavigationMenu;
            }

            set
            {
                _applicationState.ShowNavigationMenu = value;

                OnPropertyChanged();
            }
        }

        public bool IsLightTheme
        {
            get => SelectedTheme == ThemeType.Light;

            set
            {
                if (value)
                    SelectedTheme = ThemeType.Light;
                OnPropertyChanged(nameof(IsLightTheme));
                OnPropertyChanged(nameof(IsDarkTheme));
            }
        }

        public bool IsDarkTheme
        {
            get => SelectedTheme == ThemeType.Dark;

            set
            {
                if (value)
                    SelectedTheme = ThemeType.Dark;
                OnPropertyChanged(nameof(IsLightTheme));
                OnPropertyChanged(nameof(IsDarkTheme));
            }
        }

        public bool StartMaximized
        {
            get
            {
                return _applicationState.StartMaximized;
            }

            set
            {
                _applicationState.StartMaximized = value;

                OnPropertyChanged();
            }
        }

        public bool RememberLastPage
        {
            get
            {
                return _applicationState.RememberLastPage;
            }

            set
            {
                _applicationState.RememberLastPage = value;

                OnPropertyChanged();
            }
        }

        public bool StartWithWindows
        {
            get
            {
                return _applicationState.StartWithWindows;
            }

            set
            {
                _applicationState.StartWithWindows = value;

                OnPropertyChanged();
            }
        }

        private readonly ApplicationStateService _applicationState;

        public SettingsViewModel(
            ApplicationStateService applicationState)
        {
            _applicationState = applicationState;

            _themeService = new ThemeService();

            _settingsService = new SettingsService();

            SelectedTheme = _settingsService.GetTheme();
        }

        private void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}