using SQLite;

namespace MapNotepad.Models
{
    public interface IModelBase
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
    }
}
