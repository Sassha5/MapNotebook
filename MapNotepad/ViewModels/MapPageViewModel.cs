using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MapNotepad.Models;
using MapNotepad.Services.PinsManagerService;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private readonly IPinsManagerService _pinsManagerService;

        public ObservableCollection<Pin> PinCollection { get; set; }

        public MapPageViewModel(INavigationService navigationService,
                                IPinsManagerService pinsManagerService)
                                : base(navigationService)
        {
            _pinsManagerService = pinsManagerService;
            //GetLocationNameCommand = new Command<Position>(async (param) => await GetLocationName(param));
        }

        //private ICommand _MapClickedCommand;
        //public ICommand MapClickedCommand => new Command(OnMapClickedCommand);
        //public ICommand GetLocationNameCommand { get; set; }


        //private void OnMapClickedCommand(object obj)
        //{
        //    UpdateCollection();
        //}

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }


        public ICommand MapClickedCommand => new Command<MapClickedEventArgs>(OnMapClickedCommand);

        private void OnMapClickedCommand(MapClickedEventArgs args)
        {
            _pinsManagerService.AddPin(new CustomPin()
            {
                Label = $"Pin {PinCollection.Count}",
                Latitude = args.Point.Latitude,
                Longtitude = args.Point.Longitude
            });
            UpdateCollection();
            //PinCollection.Add(new Pin
            //{
            //    Label = $"Pin{PinCollection.Count}",
            //    Position = args.Point
            //});
        }

        private void UpdateCollection()
        {
            IEnumerable<CustomPin> customPins = _pinsManagerService.GetUserPins(0);
            foreach (CustomPin pin in customPins)           //TODO make map work with custom pins
            {
                PinCollection.Add(new Pin() { Label = pin.Label, Position = new Position(pin.Latitude, pin.Longtitude) });
            }
            //RaisePropertyChanged($"{nameof(PinCollection)}");
        }

        //public async Task GetLocationName(Position position)
        //{
        //    string PickupText;
        //    try
        //    {
        //        var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
        //        var placemark = placemarks?.FirstOrDefault();
        //        if (placemark != null)
        //        {
        //            PickupText = placemark.FeatureName;
        //        }
        //        else
        //        {
        //            PickupText = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.ToString());
        //    }
        //}
    }
}
