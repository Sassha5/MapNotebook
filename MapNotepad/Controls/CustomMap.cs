﻿using System;
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
            set { SetValue(PinsCollectionProperty, value); }
        }

        public CustomMap()
        {
            UiSettings.MyLocationButtonEnabled = true;
            PinsCollection = new ObservableCollection<Pin>();
            PinsCollection.CollectionChanged += Pins_CollectionChanged;
        }

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

        private static void PinsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && bindable as CustomMap != null && newValue as ObservableCollection<Pin> != null)
            {
                UpdatePins(bindable as CustomMap, newValue as ObservableCollection<Pin>);
            }
        }

    }
}
