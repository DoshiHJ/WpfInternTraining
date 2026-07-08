using POC13_NavigationApp.Commands;
using POC13_NavigationApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace POC13_NavigationApp.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private readonly ApplicationStateService _applicationState;
        private readonly INavigationService _navigationService;


        private readonly HomeViewModel _homeViewModel;

        private readonly ProductsViewModel _productsViewModel;

        private readonly SettingsViewModel _settingsViewModel;

        private string _activePage;

        public string ActivePage
        {
            get
            {
                return _activePage;
            }

            set
            {
                _activePage = value;

                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(ActivePage)));
            }
        }

        private bool _isHomeSelected;
        public bool IsHomeSelected
        {
            get => _isHomeSelected;
            set
            {
                _isHomeSelected = value;
                OnPropertyChanged(nameof(IsHomeSelected));
            }
        }

        private bool _isProductsSelected;
        public bool IsProductsSelected
        {
            get => _isProductsSelected;
            set
            {
                _isProductsSelected = value;
                OnPropertyChanged(nameof(IsProductsSelected));
            }
        }

        private bool _isSettingsSelected;
        public bool IsSettingsSelected
        {
            get => _isSettingsSelected;
            set
            {
                _isSettingsSelected = value;
                OnPropertyChanged(nameof(IsSettingsSelected));
            }
        }

        public bool ShowNavigationMenu
        {
            get
            {
                return _applicationState.ShowNavigationMenu;
            }
        }


        public Visibility NavigationVisibility
            {
                get
                {
                    return ShowNavigationMenu
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                }
            }

    public ICommand HomeCommand { get; }

        public ICommand ProductsCommand { get; }

        public ICommand SettingsCommand { get; }


        //public object CurrentViewModel
        //{
        //    get
        //    {
        //        return _navigationService.CurrentViewModel;
        //    }
        //}

        public INavigationService NavigationService {  get { return _navigationService; } }

        public MainViewModel()
        {
            _navigationService = new NavigationService();

            _homeViewModel = new HomeViewModel();

            _productsViewModel = new ProductsViewModel();

            _applicationState = new ApplicationStateService();

            _settingsViewModel =  new SettingsViewModel(_applicationState);

            HomeCommand =
                new RelayCommand(ShowHome);

            ProductsCommand =
                new RelayCommand(ShowProducts);

            SettingsCommand =
                new RelayCommand(ShowSettings);


            _applicationState.PropertyChanged += ApplicationState_PropertyChanged;

            LoadStartupPage();



            //_navigationService.PropertyChanged +=
            //    NavigationService_PropertyChanged;
        }


        private void ShowHome()
        {
            IsHomeSelected = true;

            IsProductsSelected = false;

            IsSettingsSelected = false;

            RaiseMenuProperties();

            _navigationService.Navigate(_homeViewModel);
        }

        private void ShowProducts()
        {
            IsHomeSelected = false;

            IsProductsSelected = true;

            IsSettingsSelected = false;

            RaiseMenuProperties();

            _navigationService.Navigate(_productsViewModel);
        }

        private void ShowSettings()
        {
            IsHomeSelected = false;

            IsProductsSelected = false;

            IsSettingsSelected = true;

            RaiseMenuProperties();

            _navigationService.Navigate(_settingsViewModel);
        }

        private void RaiseMenuProperties()
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(IsHomeSelected)));

            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(IsProductsSelected)));

            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(IsSettingsSelected)));
        }

        private void LoadStartupPage()
        {
            if (!Properties.Settings.Default.RememberLastPage)
            {
                ShowHome();
                return;
            }

            switch (Properties.Settings.Default.LastPage)
            {
                case "Products":
                    ShowProducts();
                    break;

                case "Settings":
                    ShowSettings();
                    break;

                default:
                    ShowHome();
                    break;
            }
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

        private void ApplicationState_PropertyChanged(
    object sender,
    PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ApplicationStateService.ShowNavigationMenu))
            {
                OnPropertyChanged(nameof(ShowNavigationMenu));

                OnPropertyChanged(nameof(NavigationVisibility));
            }
        }
    }
}