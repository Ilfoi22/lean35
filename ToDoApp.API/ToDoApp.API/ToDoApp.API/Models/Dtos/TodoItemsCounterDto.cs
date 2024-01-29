using ToDoApp.API.Data.Entities;

namespace ToDoApp.API.Models.Dtos;

public class TodoItemsCounterDto
{
    public IEnumerable<TodoItem>? Items { get; set; }
    public int TotalCount { get; set; }
}