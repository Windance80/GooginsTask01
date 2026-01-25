using System;
using GooginsTask01.Shared.Data.Models;
using GooginsTask01.Shared.Services;

namespace GooginsTask01.Web.Repositories;

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

        public Task DeleteItemAsync(Item item)
        {
            for (int i = 0; i < _todos.Count; i++)
            {
                for (int j = 0; j < _todos[i].TodoItems.Count; j++)
                {
                    if (_todos[i].TodoItems[j].Id == item.TodoItemId)
                    {
                        for (int k = 0; k < _todos[i].TodoItems[j].Items.Count; k++)
                        {
                            if (_todos[i].TodoItems[j].Items[k].Id == item.Id)
                            {
                                _todos[i].TodoItems[j].Items.RemoveAt(k);
                                Console.WriteLine($"Item: {item.Text} deleted");
                                return Task.CompletedTask;
                            }
                        }
                    }

                    // var itemTobeDelete = _todos[i].TodoItems[j].Items.Find(i => i.Id == item.Id);

                    // if (itemTobeDelete != null)
                    // {
                    //     var isDeleted = _todos[i].TodoItems[j].Items.Remove(itemTobeDelete);

                    //     if (isDeleted)
                    //     {
                    //         Console.WriteLine($"Item: {item.Text} deleted");
                    //     }
                    //     else
                    //     {
                    //         Console.WriteLine($"Item: {item.Text} not found");
                    //     }
                    // }
                }
            }
            return Task.CompletedTask;
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

        public Task DeleteTodoItemAsync(Todo todo, TodoItem todoItem)
        {
            throw new NotImplementedException();
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
            // remove and add todo
            var removeTodo = _todos.Find(td => td.Id == todo.Id);
            if (removeTodo != null)
            {
                _todos.Remove(removeTodo);
                Console.WriteLine($"UpdateTodoAsync: {todo.Id} removed");
                _todos.Add(todo);
                Console.WriteLine($"UpdateTodoAsync: {todo.Id} add");
            }
            else
            {
                Console.WriteLine($"UpdateTodoAsync: {todo.Id} not found");
            }

            return Task.CompletedTask;
        }

        public Task UpdateTodoAsync(Todo todo, TodoItem todoItem)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTodoAsync(Todo todo, TodoItem todoItem, string text)
        {
            for (int i = 0; i < todo.TodoItems.Count; i++)
            {
                if (todo.TodoItems[i].Id == todoItem.Id)
                {
                    var items = todo.TodoItems[i].Items;

                    var nextId = items.Count != 0 ? items.Max(item => item.Id) + 1 : 1;

                    todo.TodoItems[i].Items.Add(
                    new Item
                    {
                        Id = nextId,
                        IsCompleted = false,
                        Text = text,
                        TodoItem = todo.TodoItems[i],
                        TodoItemId = todo.TodoItems[i].Id,
                    }
                    );
                    i = todo.TodoItems.Count;
                }
            }
            UpdateTodoAsync(todo);
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
                new Item { Id = 1, Text = "Client standup", IsCompleted = true, TodoItemId = 1 },
                new Item { Id = 2, Text = "Review pull requests", IsCompleted = false, TodoItemId = 1 },
                new Item { Id = 3, Text = "Fix login bug", IsCompleted = false, TodoItemId = 1 }
            },
            TodoId = 1,
        },
        new TodoItem
        {
            Id = 2,
            Time = today.AddHours(14),
            Items =
            {
                new Item { Id = 4, Text = "Team retrospective", IsCompleted = false, TodoItemId = 2 }
            },
            TodoId = 1
        },

        // +5 new TodoItems for today
        new TodoItem
        {
            Id = 5,
            Time = today.AddHours(8),
            Items =
            {
                new Item
                {
                    Id = 11,
                    Text = "Check emails & Slack",
                    IsCompleted = true,
                    TodoItemId = 5
                }
            },
            TodoId = 1
         },
        new TodoItem
        {
            Id = 6,
            Time = today.AddHours(11),
            Items =
            {
                new Item
                {
                    Id = 12,
                    Text = "Code review - feature branch",
                    IsCompleted = false,
                    TodoItemId = 6,
                }
            },
            TodoId = 1
        },
        new TodoItem { Id = 7, Time = today.AddHours(13), Items = { new Item { Id = 13, Text = "Lunch + short walk", IsCompleted = true, TodoItemId = 7, } }, TodoId = 1 },
        new TodoItem { Id = 8, Time = today.AddHours(16), Items = { new Item { Id = 14, Text = "Update project roadmap", IsCompleted = false, TodoItemId = 8, } }, TodoId = 1 },
        new TodoItem { Id = 9, Time = today.AddHours(18), Items = { new Item { Id = 15, Text = "Daily wrap-up & tomorrow planning", IsCompleted = false, TodoItemId = 9, } }, TodoId = 1 }
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
                new Item { Id = 5, Text = "Grocery shopping", IsCompleted = true, TodoItemId = 3 },
                new Item { Id = 6, Text = "Call mom", IsCompleted = true, TodoItemId = 3 },
                new Item { Id = 7, Text = "Water the plants", IsCompleted = true, TodoItemId = 3 }
            },
            TodoId = 2
        },

        // +5 new TodoItems for yesterday
        new TodoItem { Id = 10, Time = yesterday.AddHours(7), Items = { new Item { Id = 16, Text = "Client workout", IsCompleted = true, TodoItemId = 10 } }, TodoId = 2 },
        new TodoItem { Id = 11, Time = yesterday.AddHours(12), Items = { new Item { Id = 17, Text = "Dentist appointment", IsCompleted = true, TodoItemId = 11 } }, TodoId = 2 },
        new TodoItem { Id = 12, Time = yesterday.AddHours(15), Items = { new Item { Id = 18, Text = "Finish quarterly report", IsCompleted = true, TodoItemId = 12 } }, TodoId = 2 },
        new TodoItem { Id = 13, Time = yesterday.AddHours(17), Items = { new Item { Id = 19, Text = "Buy birthday gift", IsCompleted = false, TodoItemId = 13 } }, TodoId = 2 },
        new TodoItem { Id = 14, Time = yesterday.AddHours(20), Items = { new Item { Id = 20, Text = "Watch new episode", IsCompleted = true, TodoItemId = 14 } }, TodoId = 2 }
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
                new Item { Id = 8, Text = "Finish project proposal", IsCompleted = true, TodoItemId = 4 },
                new Item { Id = 9, Text = "Email client update", IsCompleted = true, TodoItemId = 4 },
                new Item { Id = 10, Text = "Prepare demo", IsCompleted = false, TodoItemId = 4 }
            },
            TodoId = 3
        },

        // +5 new TodoItems for two days ago
        new TodoItem { Id = 15, Time = twoDaysAgo.AddHours(9), Items = { new Item { Id = 21, Text = "Weekly team sync", IsCompleted = true, TodoItemId = 15 } }, TodoId = 3 },
        new TodoItem { Id = 16, Time = twoDaysAgo.AddHours(11), Items = { new Item { Id = 22, Text = "Update documentation", IsCompleted = true, TodoItemId = 16 } }, TodoId = 3 },
        new TodoItem { Id = 17, Time = twoDaysAgo.AddHours(13), Items = { new Item { Id = 23, Text = "Fix CI pipeline", IsCompleted = true, TodoItemId = 17 } }, TodoId = 3 },
        new TodoItem { Id = 18, Time = twoDaysAgo.AddHours(16), Items = { new Item { Id = 24, Text = "Research new tech stack", IsCompleted = false, TodoItemId = 18 } }, TodoId = 3 },
        new TodoItem { Id = 19, Time = twoDaysAgo.AddHours(19), Items = { new Item { Id = 25, Text = "Backup important files", IsCompleted = true, TodoItemId = 19 } }, TodoId = 3 }
    });

            _todos.AddRange(new[] { todoToday, todoYesterday, todoOld });
        }

}
