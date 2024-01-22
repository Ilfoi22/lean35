using ToDoApp.API.Data.Entities;
using ToDoApp.API.Repositories.Interfaces;
using ToDoApp.API.Services.Interfaces;

namespace ToDoApp.API.Services;

public class TodoItemService : ITodoItemService
{
    private readonly ITodoItemRepository _todoItemRepository;

    public TodoItemService(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task<IEnumerable<TodoItem?>> GetTodoItemsAsync(int page, int pageSize)
    {
        var result = await _todoItemRepository.GetTodoItemsAsync(page, pageSize);
        return result;
    }

    public async Task<TodoItem?> GetTodoItemByIdAsync(Guid id)
    {
        var result = await _todoItemRepository.GetTodoItemByIdAsync(id);
        return result;
    }

    public async Task<IEnumerable<TodoItem?>> SearchTodoItemsByCategory(string category)
    {
        var result = await _todoItemRepository.SearchTodoItemsByCategory(category);
        return result;
    }

    public async Task<TodoItem?> AddTodoAsync(TodoItem todo)
    {
        var result = await _todoItemRepository.AddTodoAsync(todo);
        return result;
    }

    public async Task<TodoItem?> DeleteTodoAsync(Guid id)
    {
        var result = await _todoItemRepository.DeleteTodoAsync(id);
        return result;
    }

    public async Task<TodoItem?> UpdateTodoAsync(Guid id, TodoItem todo)
    {
        var result = await _todoItemRepository.UpdateTodoAsync(id, todo);
        return result;
    }
}
