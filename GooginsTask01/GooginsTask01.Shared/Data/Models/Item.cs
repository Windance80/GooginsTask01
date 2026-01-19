using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GooginsTask01.Shared.Data.Models;

public class Item
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [ForeignKey(typeof(TodoItem))]
    public int TodoItemId { get; set; }

    public string Text { get; set; } = string.Empty;

    // Optional: for checkboxes later
    public bool IsCompleted { get; set; }

    [ManyToOne]
    public TodoItem? TodoItem { get; set; }

    public override string ToString()
    {
        return $"Id:{Id} TodoItemid:{TodoItemId} Text:{Text} IsCompleted:{IsCompleted} TodoItem:{TodoItem}";
    }

}

