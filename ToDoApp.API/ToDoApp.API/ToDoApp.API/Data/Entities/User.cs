namespace ToDoApp.API.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<TodoItem>? TodoItems { get; set; } 
}