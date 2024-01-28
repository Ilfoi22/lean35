using ToDoApp.API.Data.Entities;

namespace ToDoApp.API.Repositories.Interfaces;

public interface ITodoItemRepository
{
    Task<IEnumerable<TodoItem?>> GetTodoItemsAsync(Guid userId, int page, int pageSize);
    Task<int> GetTotalTodoItemCountAsync(Guid userId);
    Task<TodoItem?> GetTodoItemByIdAsync(Guid id);
    Task<IEnumerable<TodoItem?>> GetCompletedTodoItemsAsync(Guid userId, int page, int pageSize);
    Task<IEnumerable<TodoItem?>> GetDeletedTodoItemsAsync(Guid userId, int page, int pageSize);
    Task<IEnumerable<TodoItem?>> SearchTodoItemsByCategory(Guid userId, string category);
    Task<TodoItem?> AddTodoAsync(TodoItem todo);
    Task<TodoItem?> DeleteTodoAsync(Guid id);
    Task<TodoItem?> UpdateTodoAsync(Guid id, TodoItem todo);
}
