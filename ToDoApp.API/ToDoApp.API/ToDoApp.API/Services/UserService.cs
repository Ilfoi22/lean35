using ToDoApp.API.Data.Entities;
using ToDoApp.API.Repositories.Interfaces;
using ToDoApp.API.Services.Interfaces;

namespace ToDoApp.API.Services;

/// <summary>
/// Provides a service layer for user management in the application, encapsulating the data access logic 
/// provided by the IUserRepository. This class implements IUserService and offers methods for operations 
/// such as adding, retrieving, and updating user information. 
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> AddUserAsync(User user)
    {
        return await _userRepository.AddUserAsync(user);
    }

    public async Task<User?> GetUserAsync(User user)
    {
        return await _userRepository.GetUserAsync(user);
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        return await _userRepository.UpdateUserAsync(user);
    }

}
