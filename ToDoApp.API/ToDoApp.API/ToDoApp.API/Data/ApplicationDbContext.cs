using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoApp.API.Data.Entities;

namespace ToDoApp.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }
}
