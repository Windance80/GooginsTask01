using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GooginsTask01.Shared.Data.Models;

public class TodoItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [ForeignKey(typeof(Todo))]
    public int TodoId { get; set; }

    public DateTime Time { get; set; }

    // One TodoItem has many sub-items
    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<Item> Items { get; set; } = new List<Item>();

    // Navigation back to parent
    [ManyToOne]
    public Todo? Todo { get; set; }
}