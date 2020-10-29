using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        private void GovnoMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCollection();
        }

        //public ICommand GetLocationNameCommand { get; set; }

        private ICommand _MapClickedCommand;
        public ICommand MapClickedCommand => _MapClickedCommand ??= new Command<MapClickedEventArgs>(OnMapClickedCommand);

        private ICommand _CollectionChangedCommand;
        public ICommand CollectionChangedCommand => _CollectionChangedCommand ??= new Command(OnCollectionChangedCommand);


        private void OnCollectionChangedCommand(object obj)
        {
            UpdateCollection();
        }

        private void OnMapClickedCommand(MapClickedEventArgs args)
        {
            AddPin(new CustomPin()
            {
                Label = $"Pin {PinCollection.Count}",
                Latitude = args.Point.Latitude,
                Longitude = args.Point.Longitude
            });
            UpdateCollection();
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
