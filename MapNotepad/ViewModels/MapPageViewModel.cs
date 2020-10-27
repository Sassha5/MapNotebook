using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        public ObservableCollection<Pin> PinCollection { get; set; }

        public MapPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GetLocationNameCommand = new Command<Position>(async (param) => await GetLocationName(param));
        }

        //private ICommand _MapClickedCommand;
        //public ICommand MapClickedCommand => new Command(OnMapClickedCommand);
        public ICommand GetLocationNameCommand { get; set; }


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
            PinCollection.Add(new Pin
            {
                Label = $"Pin{PinCollection.Count}",
                Position = args.Point
            });
        }

        private void UpdateCollection()
        {
            PinCollection = new ObservableCollection<Pin>(new List<Pin>() {
                new Pin()
                {
                    Type = PinType.Place,
                    Label = "Tokyo SKYTREE",
                    Address = "Sumida-ku, Tokyo, Japan",
                    Position = new Position(35.71d, 139.81d),
                    Rotation = 33.3f,
                    Tag = "id_tokyo"
                }
        });
        }

        public async Task GetLocationName(Position position)
        {
            string PickupText;
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    PickupText = placemark.FeatureName;
                }
                else
                {
                    PickupText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
