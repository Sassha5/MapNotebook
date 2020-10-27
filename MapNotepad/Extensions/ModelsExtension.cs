using System;
using MapNotepad.Models;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Extensions
{
    public static class ModelsExtension
    {
        public static Pin ToPin(this CustomPin customPin)
        {
            return new Pin
            {
                Label = customPin.Label,
                Position = new Position(customPin.Latitude, customPin.Longtitude)
            };
        }
    }
}
