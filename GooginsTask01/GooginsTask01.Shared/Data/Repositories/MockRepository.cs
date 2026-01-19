using GooginsTask01.Shared.Data.Models;
using GooginsTask01.Shared.Services;

public class MockRepository : ITodoRepository
{
    private readonly List<Todo> _todos = new();

    public MockRepository()
    {
        SeedData();
    }

    // seed data

    public Task<Todo> CreateTodoAsync(Todo todo)
    {
        todo.Id = _todos.Count + 1;
        _todos.Add(todo);
        return Task.FromResult(todo);
    }

    public Task DeleteTodoAsync(int id)
    {
        var isRemoved = false;
        var todo = _todos.Find(todo => todo.Id == id);
        if (todo != null)
        {
            isRemoved = _todos.Remove(todo);
        }

        Console.WriteLine("DeleteTodo: " + id + " " + isRemoved);
        return Task.CompletedTask;
    }

    public Task<List<Todo>> GetAllTodosAsync()
    {
        return Task.FromResult(_todos);
    }

    public Task<Todo?> GetTodoByDateAsync(DateTime date)
    {
        var todo = _todos.Find(todo => todo.Date == date);
        return Task.FromResult(todo);
    }

    public Task<Todo?> GetTodoByIdAsync(int id)
    {
        var todo = _todos.Find(todo => todo.Id == id);

        return Task.FromResult(todo);
    }

    public Task UpdateTodoAsync(Todo todo)
    {
        var removingTodo = _todos.Find(td => td.Id == todo.Id);
        if (removingTodo != null)
        {
            _todos.Remove(removingTodo);
            _todos.Add(todo);
        }
        return Task.CompletedTask;
    }

    private void SeedData()
    {
        var today = DateTime.Today;
        var yesterday = today.AddDays(-1);
        var twoDaysAgo = today.AddDays(-2);

        // Today
        var todoToday = new Todo
        {
            Id = 1,
            Date = today,
            LastModified = DateTime.UtcNow.AddHours(-2)
        };
        todoToday.TodoItems.AddRange(new[]
        {
            new TodoItem
            {
                Id = 1,
                Time = today.AddHours(9),
                Items =
                {
                    new Item { Id = 1, Text = "Morning standup", IsCompleted = true },
                    new Item { Id = 2, Text = "Review pull requests", IsCompleted = false },
                    new Item { Id = 3, Text = "Fix login bug", IsCompleted = false }
                }
            },
            new TodoItem
            {
                Id = 2,
                Time = today.AddHours(14),
                Items =
                {
                    new Item { Id = 4, Text = "Team retrospective", IsCompleted = false }
                }
            }
        });

        // Yesterday
        var todoYesterday = new Todo
        {
            Id = 2,
            Date = yesterday,
            LastModified = yesterday.AddHours(18)
        };
        todoYesterday.TodoItems.AddRange(new[]
        {
            new TodoItem
            {
                Id = 3,
                Time = yesterday.AddHours(10),
                Items =
                {
                    new Item { Id = 5, Text = "Grocery shopping", IsCompleted = true },
                    new Item { Id = 6, Text = "Call mom", IsCompleted = true },
                    new Item { Id = 7, Text = "Water the plants", IsCompleted = true }
                }
            }
        });

        // Older
        var todoOld = new Todo
        {
            Id = 3,
            Date = twoDaysAgo,
            LastModified = twoDaysAgo.AddHours(20),
            IsDeleted = false
        };
        todoOld.TodoItems.Add(new TodoItem
        {
            Id = 4,
            Time = twoDaysAgo.AddHours(15),
            Items =
            {
                new Item { Id = 8, Text = "Finish project proposal", IsCompleted = true },
                new Item { Id = 9, Text = "Email client update", IsCompleted = true },
                new Item { Id = 10, Text = "Prepare demo", IsCompleted = false }
            }
        });

        _todos.AddRange(new[] { todoToday, todoYesterday, todoOld });
    }

}