using ToDoApp.API.Data.Entities;

namespace ToDoApp.API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> AddUserAsync(User user);
    Task<User?> GetUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
}