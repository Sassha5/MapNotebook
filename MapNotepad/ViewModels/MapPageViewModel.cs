using System;
using System.Linq;
using MapNotepad.Models;
using MapNotepad.Services.PinsManagerService;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class MapPageViewModel : ViewModelMapBase
    {
        private Pin _selectedPin;
        public Pin SelectedPin
        {
            get => _selectedPin;
            set => SetProperty(ref _selectedPin, value);
        }


        public MapPageViewModel(INavigationService navigationService,
                                IPinsManagerService pinsManagerService)
                                : base(navigationService, pinsManagerService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.TryGetValue(nameof(CustomPin), out CustomPin pin))
            {
                SelectedPin = PinCollection.FirstOrDefault(x => x.Label == pin.Label);
                CameraPosition = new Position(pin.Latitude, pin.Longitude);
            }
        }
    }
}
