using Microsoft.EntityFrameworkCore;
using ToDoApp.API.Data;
using ToDoApp.API.Data.Entities;
using ToDoApp.API.Repositories.Interfaces;

namespace ToDoApp.API.Repositories;

/// <summary>
/// The UserRepository class is responsible for managing user data within the application.
/// It implements the IUserRepository interface and provides asynchronous methods for adding,
/// retrieving, and updating user information in the database.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<IUserRepository> _logger;

    public UserRepository(ApplicationDbContext context, ILogger<IUserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<User?> AddUserAsync(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

        if (existingUser != null)
        {
            return null;
        }

        var item = await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"\n------------\nMessage from {nameof(IUserRepository)}:\nSuccessfully added user with ID {user.Id}\n------------");

        return item.Entity;
    }


    public async Task<User?> GetUserAsync(User user)
    {
        var foundUser = await _context.Users
            .Where(u => u.Username == user.Username && u.Password == user.Password)
            .FirstOrDefaultAsync();

        if (foundUser != null)
        {
            _logger.LogInformation($"\n------------\nMessage from {nameof(IUserRepository)}:\nSuccessfully get user with ID {foundUser.Id}\n------------");
        }

        return foundUser;
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (existingUser == null)
        {
            return null;
        }

        existingUser.Token = user.Token;

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();

        return existingUser;
    }

}
