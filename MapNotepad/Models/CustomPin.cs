using System;
using SQLite;

namespace MapNotepad.Models
{
    [Table("Pins")]
    public class CustomPin : BaseModel
    {
        public string Label { get; set; }
        public string Adress { get; set; }
        public int UserId { get; set; }
        public string Reminder { get; set; }
        public bool IsFavorite { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}
