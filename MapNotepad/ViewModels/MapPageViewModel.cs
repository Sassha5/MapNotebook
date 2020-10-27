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
    public class MapPageViewModel : ViewModelCollectionBase
    {

        public MapPageViewModel(INavigationService navigationService,
                                IPinsManagerService pinsManagerService)
                                : base(navigationService, pinsManagerService)
        {
            //GetLocationNameCommand = new Command<Position>(async (param) => await GetLocationName(param));
        }

        //public ICommand GetLocationNameCommand { get; set; }


        public ICommand MapClickedCommand => new Command<MapClickedEventArgs>(OnMapClickedCommand);

        private void OnMapClickedCommand(MapClickedEventArgs args)
        {
            AddPin(new CustomPin()
            {
                Label = $"Pin {PinCollection.Count}",
                Latitude = args.Point.Latitude,
                Longtitude = args.Point.Longitude
            });
            UpdateCollection();
        }

        //private void UpdateCollection()
        //{
        //    IEnumerable<CustomPin> customPins = _pinsManagerService.GetUserPins(0);
        //    foreach (CustomPin pin in customPins)           //TODO make map work with custom pins
        //    {
        //        PinCollection.Add(new Pin() { Label = pin.Label, Position = new Position(pin.Latitude, pin.Longtitude) });
        //    }
        //}

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
