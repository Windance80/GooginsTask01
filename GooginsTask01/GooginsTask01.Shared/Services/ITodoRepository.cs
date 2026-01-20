
using GooginsTask01.Shared.Data.Models;

namespace GooginsTask01.Shared.Services;
public interface ITodoRepository
{
    Task<List<Todo>> GetAllTodosAsync();
    Task<Todo?> GetTodoByIdAsync(int id);
    Task<Todo?> GetTodoByDateAsync(DateTime date);
    Task<Todo> CreateTodoAsync(Todo todo);
    Task UpdateTodoAsync(Todo todo);
    Task DeleteTodoAsync(int id);       
    Task DeleteItemAsync(Item item);
}