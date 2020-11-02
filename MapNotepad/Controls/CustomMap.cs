using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            PinsCollection = new ObservableCollection<Pin>();
            PinsCollection.CollectionChanged += Pins_CollectionChanged;
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
            set => SetValue(PinsCollectionProperty, value);
        }
        #endregion

        #region Helpers
        private void Pins_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePins(this, sender as IEnumerable<Pin>);
        }

        private static void UpdatePins(CustomMap map, IEnumerable<Pin> newPins)
        {
            map.Pins.Clear();
            foreach (var pin in newPins)
            {
                map.Pins.Add(pin);
            }
        }

        private static void UpdateCameraPosition(CustomMap map, Position cameraPosition)
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius(cameraPosition, Distance.FromMiles(5)));
        }

        private static void CameraPositionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && bindable as CustomMap != null && newValue != null)
            {
                UpdateCameraPosition(bindable as CustomMap, (Position)newValue);
            }
        }

        private static void PinsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && bindable as CustomMap != null && newValue as ObservableCollection<Pin> != null)
            {
                UpdatePins(bindable as CustomMap, newValue as ObservableCollection<Pin>);
            }
        }
        #endregion
    }
}
