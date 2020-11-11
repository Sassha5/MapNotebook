using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using MapNotepad.Controls;
using MapNotepad.Models;
using MapNotepad.Models.Weather;
using MapNotepad.Services.PermissionService;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Services.ThemeService;
using MapNotepad.Services.WeatherService;
using Plugin.LocalNotifications;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class MapPageViewModel : ViewModelMapBase
    {
        private IWeatherService _weatherService;

        public MapPageViewModel(INavigationService navigationService,
                               IPinsManagerService pinsManagerService,
                               IThemeService themeManagerService,
                               IWeatherService weatherService,
                               IPermissionService permissionService)
                               : base(navigationService,
                                     pinsManagerService,
                                     themeManagerService,
                                     permissionService)
        {
            _weatherService = weatherService;
        }

        #region Properties

        private CustomPin _selectedCustomPin;
        public CustomPin SelectedCustomPin
        {
            get => _selectedCustomPin;
            set
            {
                SetProperty(ref _selectedCustomPin, value);
                SetWeatherData(value);
            }
        }

        private WeatherForecast _weatherForecast;
        public WeatherForecast WeatherForecast
        {
            get => _weatherForecast;
            set => SetProperty(ref _weatherForecast, value);
        }

        #endregion

        private ICommand _selectedPinChangedCommand;
        public ICommand SelectedPinChangedCommand => _selectedPinChangedCommand ??= new Command<object>(OnSelectedPinChangedCommand);

        #region INavigatedAware override

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            if (parameters.TryGetValue(nameof(CustomPin), out CustomPin pin))
            {
                SelectedCustomPin = pin;
                CameraPosition = new Position(pin.Latitude, pin.Longitude);
            }
        }

        #endregion

        private void OnSelectedPinChangedCommand(object pinObj)
        {
            if (pinObj is Pin pin)
            {
                SelectedCustomPin = CustomPinCollection.FirstOrDefault(x => x.Label == pin.Label);
            }
        }

        private async void SetWeatherData(CustomPin pin)
        {
            if (pin != null)
            {
                WeatherForecast = await _weatherService.GetWeatherForecast(pin.Latitude, pin.Longitude);
            }
        }
    }
}
