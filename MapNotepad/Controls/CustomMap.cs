using System;
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
            CustomPinsCollection = new ObservableCollection<CustomPin>();
        }

        #region Properties
        public static readonly BindableProperty PinsCollectionProperty =
            BindableProperty.Create(
                propertyName: nameof(PinsCollection),
                returnType: typeof(ObservableCollection<Pin>),
                declaringType: typeof(CustomMap),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: PinsPropertyChanged);

        public ObservableCollection<Pin> PinsCollection
        {
            get => (ObservableCollection<Pin>)GetValue(PinsCollectionProperty);
            set => SetValue(PinsCollectionProperty, value);
        }

        public static readonly BindableProperty CustomPinsCollectionProperty =
            BindableProperty.Create(
                propertyName: nameof(CustomPinsCollection),
                returnType: typeof(ObservableCollection<CustomPin>),
                declaringType: typeof(CustomMap),
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: CustomPinsPropertyChanged);

        public ObservableCollection<CustomPin> CustomPinsCollection
        {
            get => (ObservableCollection<CustomPin>)GetValue(CustomPinsCollectionProperty);
            set => SetValue(CustomPinsCollectionProperty, value);
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

        #endregion

        #region Helpers

        private static void CameraPositionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as CustomMap;
            var newCameraPosition = (Position)newValue;
            if (newValue != oldValue && map != null && newCameraPosition != null)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(newCameraPosition, Distance.FromMiles(5)));
            }
        }


        private static void CustomPinsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
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

        private static void PinsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var map = bindable as CustomMap;
            var newPins = newValue as ObservableCollection<Pin>;
            if (newValue != oldValue && map != null && newPins != null)
            {
                map.Pins.Clear();
                foreach (var pin in newPins)
                {
                    map.Pins.Add(pin);
                }
            }
        }

        #endregion
    }
}
