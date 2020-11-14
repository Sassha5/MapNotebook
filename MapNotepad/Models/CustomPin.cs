using System;
using SQLite;

namespace MapNotepad.Models
{
    [Table("Pins")]
    public class CustomPin : IModelBase
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Adress { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsFavorite { get; set; } = true;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string IsFavoriteImagePath { get => IsFavorite ? "ic_fav.png" : "ic_notfav.png"; }
    }
}
