using System.Collections.ObjectModel;
using MapNotepad.Extensions;
using MapNotepad.Models;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering;

namespace MapNotepad.Controls
{
    public class CustomMap : ClusteredMap
    {
        public CustomMap()
        {
            UiSettings.MyLocationButtonEnabled = true;
        }

        #region -- Public Properties --

        public static readonly BindableProperty CustomPinCollectionProperty =
            BindableProperty.Create(
                propertyName: nameof(CustomPinCollection),
                returnType: typeof(ObservableCollection<CustomPin>),
                declaringType: typeof(CustomMap),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: CustomPinCollectionPropertyChanged);

        public ObservableCollection<CustomPin> CustomPinCollection
        {
            get => (ObservableCollection<CustomPin>)GetValue(CustomPinCollectionProperty);
            set => SetValue(CustomPinCollectionProperty, value);
        }

        public static readonly BindableProperty MapCameraPositionProperty =
            BindableProperty.Create(
                propertyName: nameof(MapCameraPosition),
                returnType: typeof(Position),
                declaringType: typeof(CustomMap),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: CameraPositionPropertyChanged);

        public Position MapCameraPosition
        {
            get => (Position)GetValue(MapCameraPositionProperty);
            set => SetValue(MapCameraPositionProperty, value);
        }

        public static readonly BindableProperty SelectedCustomPinProperty =
            BindableProperty.Create(
                propertyName: nameof(SelectedCustomPin),
                returnType: typeof(CustomPin),
                declaringType: typeof(CustomMap),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: SelectedCustomPinPropertyChanged);

        public CustomPin SelectedCustomPin
        {
            get => (CustomPin)GetValue(SelectedCustomPinProperty);
            set => SetValue(SelectedCustomPinProperty, value);
        }

        #endregion

        #region -- Private Helpers --

        private static void CameraPositionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as CustomMap;
            var newCameraPosition = (Position)newValue;
            if (newValue != oldValue && map != null && newCameraPosition != null)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(newCameraPosition, Distance.FromMiles(5)));
            }
        }

        private static void CustomPinCollectionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as CustomMap;
            var newPins = newValue as ObservableCollection<CustomPin>;

            if (newValue != oldValue && map != null && newPins != null)
            {
                map.Pins.Clear();
                foreach (var pin in newPins)
                {
                    map.Pins.Add(pin.ToPin());
                }
            }
        }

        private static void SelectedCustomPinPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as CustomMap;
            var newSelectedPin = (CustomPin)newValue;
            if (newValue != oldValue && map != null && newSelectedPin != null)
            {
                map.SelectedPin = newSelectedPin.ToPin();
            }
        }

        #endregion
    }
}
