using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GooginsTask01.Shared.Data.Models;

public class Todo
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime LastModified { get; set; }
    public bool IsDeleted { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}