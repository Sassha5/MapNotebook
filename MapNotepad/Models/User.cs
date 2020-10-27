using System;
using SQLite;

namespace MapNotepad.Models
{
    [Table("Users")]
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
