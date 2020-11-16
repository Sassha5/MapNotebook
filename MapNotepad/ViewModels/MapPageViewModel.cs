using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MapNotepad.Models;
using MapNotepad.Services.PermissionService;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Services.ThemeService;
using MapNotepad.Services.WeatherService;
using Prism.Navigation;
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

        #region -- Public Properties --

        private CustomPin _selectedCustomPin;
        public CustomPin SelectedCustomPin
        {
            get => _selectedCustomPin;
            set => SetProperty(ref _selectedCustomPin, value);
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

        #region -- Overrides --

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            if (parameters.TryGetValue(nameof(CustomPin), out CustomPin pin))
            {
                SearchBarText = string.Empty;
                CameraPosition = new Position(pin.Latitude, pin.Longitude);
            }
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(SelectedCustomPin))
            {
                await SetWeatherData(_selectedCustomPin);
            }
        }

        protected override async Task<IEnumerable<CustomPin>> GetPinsAsync()
        {
            var pins = await base.GetPinsAsync();
            return pins.Where(x => x.IsFavorite);
        }

        #endregion

        #region -- Private Helpers --

        private void OnSelectedPinChangedCommand(object pinObj)
        {
            if (pinObj is Pin pin)
            {
                SelectedCustomPin = CustomPinCollection.FirstOrDefault(x => x.Label == pin.Label);
            }
        }

        private async Task SetWeatherData(CustomPin pin)
        {
            if (pin != null)
            {
                WeatherForecast = await _weatherService.GetWeatherForecast(pin.Latitude, pin.Longitude);
            }
        }

        #endregion
    }
}
