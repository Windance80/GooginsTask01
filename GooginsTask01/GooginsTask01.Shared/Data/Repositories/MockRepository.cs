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
            for (int i = 0; i < todo.TodoItems.Count; i++)
            {
                for (int j = 0; j < todo.TodoItems[i].Items.Count; j++)
                {
                    todo.TodoItems[i].Items[j].Id = j + 1;
                }

            }
        }
        _todos.Add(todo);

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
        // Original
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
        },

        // +5 new TodoItems for today
        new TodoItem { Id = 5, Time = today.AddHours(8), Items = { new Item { Id = 11, Text = "Check emails & Slack", IsCompleted = true } } },
        new TodoItem { Id = 6, Time = today.AddHours(11), Items = { new Item { Id = 12, Text = "Code review - feature branch", IsCompleted = false } } },
        new TodoItem { Id = 7, Time = today.AddHours(13), Items = { new Item { Id = 13, Text = "Lunch + short walk", IsCompleted = true } } },
        new TodoItem { Id = 8, Time = today.AddHours(16), Items = { new Item { Id = 14, Text = "Update project roadmap", IsCompleted = false } } },
        new TodoItem { Id = 9, Time = today.AddHours(18), Items = { new Item { Id = 15, Text = "Daily wrap-up & tomorrow planning", IsCompleted = false } } }
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
        // Original
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
        },

        // +5 new TodoItems for yesterday
        new TodoItem { Id = 10, Time = yesterday.AddHours(7), Items = { new Item { Id = 16, Text = "Morning workout", IsCompleted = true } } },
        new TodoItem { Id = 11, Time = yesterday.AddHours(12), Items = { new Item { Id = 17, Text = "Dentist appointment", IsCompleted = true } } },
        new TodoItem { Id = 12, Time = yesterday.AddHours(15), Items = { new Item { Id = 18, Text = "Finish quarterly report", IsCompleted = true } } },
        new TodoItem { Id = 13, Time = yesterday.AddHours(17), Items = { new Item { Id = 19, Text = "Buy birthday gift", IsCompleted = false } } },
        new TodoItem { Id = 14, Time = yesterday.AddHours(20), Items = { new Item { Id = 20, Text = "Watch new episode", IsCompleted = true } } }
    });

        // Two days ago
        var todoOld = new Todo
        {
            Id = 3,
            Date = twoDaysAgo,
            LastModified = twoDaysAgo.AddHours(20),
            IsDeleted = false
        };
        todoOld.TodoItems.AddRange(new[]
        {
        // Original
        new TodoItem
        {
            Id = 4,
            Time = twoDaysAgo.AddHours(15),
            Items =
            {
                new Item { Id = 8, Text = "Finish project proposal", IsCompleted = true },
                new Item { Id = 9, Text = "Email client update", IsCompleted = true },
                new Item { Id = 10, Text = "Prepare demo", IsCompleted = false }
            }
        },

        // +5 new TodoItems for two days ago
        new TodoItem { Id = 15, Time = twoDaysAgo.AddHours(9), Items = { new Item { Id = 21, Text = "Weekly team sync", IsCompleted = true } } },
        new TodoItem { Id = 16, Time = twoDaysAgo.AddHours(11), Items = { new Item { Id = 22, Text = "Update documentation", IsCompleted = true } } },
        new TodoItem { Id = 17, Time = twoDaysAgo.AddHours(13), Items = { new Item { Id = 23, Text = "Fix CI pipeline", IsCompleted = true } } },
        new TodoItem { Id = 18, Time = twoDaysAgo.AddHours(16), Items = { new Item { Id = 24, Text = "Research new tech stack", IsCompleted = false } } },
        new TodoItem { Id = 19, Time = twoDaysAgo.AddHours(19), Items = { new Item { Id = 25, Text = "Backup important files", IsCompleted = true } } }
    });

        _todos.AddRange(new[] { todoToday, todoYesterday, todoOld });
    }

}