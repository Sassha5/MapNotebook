using System;
using System.Linq;
using System.Threading.Tasks;
using MapNotepad.Models;
using MapNotepad.Services.PinsManagerService;
using MapNotepad.Services.ThemeManagerService;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class MapPageViewModel : ViewModelMapBase
    {
        public MapPageViewModel(INavigationService navigationService,
                               IPinsManagerService pinsManagerService,
                               IThemeManagerService themeManagerService)
                               : base(navigationService,
                                     pinsManagerService,
                                     themeManagerService)
        {
        }

        #region Properties

        private Pin _selectedPin;
        public Pin SelectedPin
        {
            get => _selectedPin;
            set
            {
                SetProperty(ref _selectedPin, value);
                Description = _selectedPin != null ? _selectedPin.Tag.ToString() : string.Empty;
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        #endregion

        #region INavigatedAware override

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);
            if (parameters.TryGetValue(nameof(CustomPin), out CustomPin pin))
            {
                SelectedPin = PinCollection.FirstOrDefault(x => x.Label == pin.Label);
                CameraPosition = new Position(pin.Latitude, pin.Longitude);
            }
        }

        #endregion
    }
}
