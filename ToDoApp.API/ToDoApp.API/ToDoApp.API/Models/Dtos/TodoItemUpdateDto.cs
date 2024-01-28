namespace ToDoApp.API.Models.Dtos;

public class TodoItemUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsDeleted { get; set; }
}
