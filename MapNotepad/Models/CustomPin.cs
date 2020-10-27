using System;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Models
{
    public class CustomPin : BaseModel
    {
        private Pin _pin;

        public CustomPin(Pin pin)
        {
            _pin = pin;
        }

        public string Adress
        {
            get => _pin.Address;
            set
            {
                _pin.Address = value;
            }
        }

        public int UserId { get; set; }
        public string Reminder { get; set; }
    }
}
