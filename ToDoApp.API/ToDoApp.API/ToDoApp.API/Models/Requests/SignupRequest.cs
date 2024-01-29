using NSwag.Annotations;
using System.Text.Json.Serialization;

namespace ToDoApp.API.Models.Requests;

public class SignupRequest
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Token { get; set; }
}
