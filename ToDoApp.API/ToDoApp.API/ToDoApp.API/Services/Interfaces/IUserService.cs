using ToDoApp.API.Data.Entities;

namespace ToDoApp.API.Services.Interfaces;

public interface IUserService
{
    Task<User?> AddUserAsync(User user);
    Task<User?> GetUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
}
