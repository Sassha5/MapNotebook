using System;
using SQLite;

namespace MapNotepad.Models
{
    [Table("Pins")]
    public class CustomPin : IModelBase
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Label { get; set; }
        public string Adress { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }
        public bool IsFavorite { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string IsFavoriteImagePath { get => IsFavorite ? "ic_fav.png" : "ic_notfav.png"; }
    }
}
