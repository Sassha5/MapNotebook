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
        public MapPageViewModel(INavigationService navigationService,
                               IPinsManagerService pinsManagerService)
                               : base(navigationService, pinsManagerService)
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
                //Description = _selectedPin.Tag.ToString(); //exception when navigating from pinpage
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.TryGetValue(nameof(CustomPin), out CustomPin pin))
            {
                SelectedPin = PinCollection.FirstOrDefault(x => x.Label == pin.Label);
                CameraPosition = new Position(pin.Latitude, pin.Longitude);
            }
        }

        #endregion
    }
}
