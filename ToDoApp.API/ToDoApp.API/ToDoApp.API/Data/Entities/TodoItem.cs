﻿namespace ToDoApp.API.Data.Entities;

public class TodoItem
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CompletedDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime DeletedDate { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; } 
}
