﻿using ToDoApp.API.Data.Entities;

namespace ToDoApp.API.Repositories.Interfaces;

public interface ITodoItemRepository
{
    Task<IEnumerable<TodoItem?>> GetTodoItemsAsync(int page, int pageSize);
    Task<TodoItem?> GetTodoItemByIdAsync(Guid id);
    Task<IEnumerable<TodoItem?>> SearchTodoItemsByCategory(string category);
    Task<TodoItem?> AddTodoAsync(TodoItem todo);
    Task<TodoItem?> DeleteTodoAsync(Guid id);
    Task<TodoItem?> UpdateTodoAsync(Guid id, TodoItem todo);
}