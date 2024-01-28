using ToDoApp.API.Data.Entities;
using ToDoApp.API.Repositories.Interfaces;
using ToDoApp.API.Services.Interfaces;

namespace ToDoApp.API.Services;

/// <summary>
/// Provides a service layer for managing Todo items, abstracting the data access logic performed by the TodoItemRepository.
/// This class implements ITodoItemService and offers asynchronous methods for operations such as retrieving, adding, 
/// deleting, and updating Todo items, as well as specific queries like fetching deleted items and searching by category.
/// </summary>
public class TodoItemService : ITodoItemService
{
    private readonly ITodoItemRepository _todoItemRepository;

    public TodoItemService(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task<IEnumerable<TodoItem?>> GetTodoItemsAsync(Guid userId, int page, int pageSize)
    {
        return await _todoItemRepository.GetTodoItemsAsync(userId, page, pageSize);
    }

    public async Task<int> GetTotalTodoItemCountAsync(Guid userId)
    {
        return await _todoItemRepository.GetTotalTodoItemCountAsync(userId);
    }

    public async Task<IEnumerable<TodoItem?>> GetCompletedTodoItemsAsync(Guid userId, int page, int pageSize)
    {
        return await _todoItemRepository.GetCompletedTodoItemsAsync(userId, page, pageSize);
    }

    public async Task<IEnumerable<TodoItem?>> GetDeletedTodoItemsAsync(Guid userId, int page, int pageSize)
    {
        return await _todoItemRepository.GetDeletedTodoItemsAsync(userId, page, pageSize);
    }

    public async Task<TodoItem?> GetTodoItemByIdAsync(Guid id)
    {
        return await _todoItemRepository.GetTodoItemByIdAsync(id);
    }

    public async Task<IEnumerable<TodoItem?>> SearchTodoItemsByCategory(Guid userId, string category)
    {
        return await _todoItemRepository.SearchTodoItemsByCategory(userId, category);
    }

    public async Task<TodoItem?> AddTodoAsync(TodoItem todo)
    {
        return await _todoItemRepository.AddTodoAsync(todo);
    }

    public async Task<TodoItem?> DeleteTodoAsync(Guid id)
    {
        return await _todoItemRepository.DeleteTodoAsync(id);
    }

    public async Task<TodoItem?> UpdateTodoAsync(Guid id, TodoItem todo)
    {
        return await _todoItemRepository.UpdateTodoAsync(id, todo);
    }
}
