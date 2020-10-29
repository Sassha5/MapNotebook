using System;
using Xamarin.Forms.GoogleMaps.Clustering;

namespace MapNotepad.Controls
{
    public class CustomMap : ClusteredMap
    {
        public CustomMap()
        {
            UiSettings.MyLocationButtonEnabled = true;
        }
    }
}
