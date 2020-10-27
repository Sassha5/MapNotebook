using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MapNotepad.Models;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Extensions
{
    public static class IEnumerableExtension
    {
        public static ObservableCollection<Pin> ToObservableCollection(this IEnumerable<CustomPin> pins)
        {
            return new ObservableCollection<Pin>(pins.Select(x => x.ToPin()));
        }
    }
}
