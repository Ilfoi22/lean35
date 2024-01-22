using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ToDoApp.API.Data;
using ToDoApp.API.Data.Entities;
using ToDoApp.API.Repositories.Interfaces;

namespace ToDoApp.API.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TodoItemRepository> _logger;

    public TodoItemRepository(ApplicationDbContext context, ILogger<TodoItemRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TodoItem?>> GetTodoItemsAsync(int page, int pageSize)
    {
        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemRepository)}:\nSuccessfully get list of items (Page: {page}, PageSize: {pageSize})\n------------");

        return await _context.TodoItems
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<TodoItem?> GetTodoItemByIdAsync(Guid id)
    {
        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemRepository)}:\nSuccessfully get item with ID {id}\n------------");

        return await _context.TodoItems
            .Where(item => item.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TodoItem?>> SearchTodoItemsByCategory(string category)
    {
        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemRepository)}:\nSearching for items by category: {category}\n------------");

        return await _context.TodoItems
            .Where(item => item.Category == category)
            .ToListAsync();
    }

    public async Task<TodoItem?> AddTodoAsync(TodoItem todo)
    {
        todo.CreatedDate = DateTime.UtcNow;

        var item = await _context.AddAsync(todo);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemRepository)}:\nItem has been successfully added to DB\n------------");

        return item.Entity;
    }


    public async Task<TodoItem?> DeleteTodoAsync(Guid id)
    {
        var item = await _context.TodoItems.FindAsync(id);

        if (item is null)
        {
            return new TodoItem { };
        }

        _context.TodoItems.Remove(item);

        await _context.SaveChangesAsync();

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemRepository)}:\nItem with ID {id} has been successfully deleted from DB\n------------");

        return item;
    }

    public async Task<TodoItem?> UpdateTodoAsync(Guid id, TodoItem todo)
    {
        var item = await _context.TodoItems.FindAsync(id);

        if (item is null)
        {
            return new TodoItem { };
        }

        item.Title = todo.Title;
        item.Description = todo.Description;
        item.Category = todo.Category;
        item.IsCompleted = todo.IsCompleted;
        item.CompletedDate = DateTime.Now;

        await _context.SaveChangesAsync();

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemRepository)}:\nItem has been successfully updated in DB\n------------");

        return item;
    }
}
