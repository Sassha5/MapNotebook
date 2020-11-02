using System;
using SQLite;

namespace MapNotepad.Models
{
    [Table("Users")]
    public class User : IModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
