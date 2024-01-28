using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ToDoApp.API.Data;
using ToDoApp.API.Data.Entities;
using ToDoApp.API.Repositories.Interfaces;

namespace ToDoApp.API.Repositories;

/// <summary>
/// Represents the repository for managing Todo items within the application.
/// Provides methods for CRUD operations on Todo items, including adding, deleting,
/// updating, and retrieving Todo items, both in a paginated fashion and by specific criteria.
/// </summary>
public class TodoItemRepository : ITodoItemRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TodoItemRepository> _logger;

    public TodoItemRepository(ApplicationDbContext context, ILogger<TodoItemRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TodoItem?>> GetTodoItemsAsync(Guid userId, int page, int pageSize)
    {
        _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemRepository)}:\nSuccessfully get list of items for User with ID: {userId}\n Page: {page}, PageSize: {pageSize})\n------------");

        return await _context.TodoItems
            .Where(item => item.UserId == userId)
            .OrderByDescending(item => item.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalTodoItemCountAsync(Guid userId)
    {
        _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemRepository)}:\nSuccessfully count items for User with ID: {userId}\n------------");

        return await _context.TodoItems
            .Where(item => item.UserId == userId)
            .CountAsync();
    }

    public async Task<IEnumerable<TodoItem?>> GetCompletedTodoItemsAsync(Guid userId, int page, int pageSize)
    {
        return await _context.TodoItems
            .Where(item => item.UserId == userId && item.IsCompleted == true)
            .OrderByDescending(item => item.CompletedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<TodoItem?>> GetDeletedTodoItemsAsync(Guid userId, int page, int pageSize)
    {
        _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemRepository)}:\nSuccessfully get list of deleted items for User with ID: {userId}\n Page: {page}, PageSize: {pageSize})\n------------");

        return await _context.TodoItems
            .Where(item => item.UserId == userId && item.IsDeleted == true)
            .OrderByDescending(item => item.DeletedDate)
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

    public async Task<IEnumerable<TodoItem?>> SearchTodoItemsByCategory(Guid userId, string category)
    {
        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemRepository)}:\nSearching for items by category: {category} for User ID: {userId}\n------------");

        return await _context.TodoItems
            .Where(item => item.UserId == userId && item.Category == category)
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
            return null;
        }

        item.Title = todo.Title;
        item.Description = todo.Description;
        item.Category = todo.Category;
        item.IsCompleted = todo.IsCompleted;
        if (todo.IsCompleted)
        {
            item.CompletedDate = DateTime.UtcNow;
        }
        item.IsDeleted = todo.IsDeleted;
        if (todo.IsDeleted)
        {
            item.DeletedDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemRepository)}:\nItem has been successfully updated in DB\n------------");

        return item;
    }
}
