using System;
using SQLite;

namespace MapNotepad.Models
{
    [Table("Users")]
    public class User : IModelBase
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public double LastMapPositionX { get; set; }
        public double LastMapPositionY { get; set; }
    }
}
