namespace ToDoApp.API.Models.Dtos;

public class TodoItemCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
}