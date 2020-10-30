using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
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

        public Pin IncomingPin
        {
            get { return (Pin)GetValue(IncomingPinProperty); }
            set { SetValue(IncomingPinProperty, value); }
        }

        public static readonly BindableProperty IncomingPinProperty = BindableProperty.Create(
                                                         propertyName: nameof(IncomingPin),
                                                         returnType: typeof(Pin),
                                                         declaringType: typeof(CustomMap),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.Default,
                                                         validateValue: null);


        public MapSpan MapSpan
        {
            get { return (MapSpan)GetValue(MapSpanProperty); }
            set { SetValue(MapSpanProperty, value); }
        }

        public static readonly BindableProperty MapSpanProperty = BindableProperty.Create(
                                                         propertyName: "MapSpan",
                                                         returnType: typeof(MapSpan),
                                                         declaringType: typeof(CustomMap),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         validateValue: null,
                                                         propertyChanged: MapSpanPropertyChanged);

        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as CustomMap;
            var newMapSpan = newValue as MapSpan;

            thisInstance?.MoveToRegion(newMapSpan);
        }

        private void PinsSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePinsSource(this, sender as IEnumerable<Pin>);
        }

        private static void UpdatePinsSource(Map CustomMap, IEnumerable<Pin> newSource)
        {
            CustomMap.Pins.Clear();
            foreach (var pin in newSource)
                CustomMap.Pins.Add(pin);
        }


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IncomingPin) && IncomingPin != null)
            {
                Pins.Add(IncomingPin);
            }
        }
    }
}
