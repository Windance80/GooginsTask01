using SQLite;
using SQLiteNetExtensions.Attributes;
using TodoList.Shared.Models;

namespace GooginsTask01.Shared.Models;

public class Todo
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    public DateTime Date { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}