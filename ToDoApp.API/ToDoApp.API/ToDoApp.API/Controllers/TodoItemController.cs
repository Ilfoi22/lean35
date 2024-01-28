using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoApp.API.Data.Entities;
using ToDoApp.API.Models.Dtos;
using ToDoApp.API.Services.Interfaces;

namespace ToDoApp.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TodoItemController : ControllerBase
{
    private readonly ITodoItemService _todoItemService;
    private readonly ILogger<TodoItemController> _logger;

    public TodoItemController(
        ILogger<TodoItemController> logger,
        ITodoItemService todoItemService)
    {
        _logger = logger;
        _todoItemService = todoItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodoItemsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        Guid userId = GetUserIdFromClaims();
        var items = await _todoItemService.GetTodoItemsAsync(userId, page, pageSize);
        var totalCount = await _todoItemService.GetTotalTodoItemCountAsync(userId);

        var response = new TodoItemsCounterDto
        {
            Items = items,
            TotalCount = totalCount
        };

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nRetrieved list of items successfully for User ID {userId} {nameof(GetTodoItemByIdAsync)} request\n------------");

        return Ok(response);
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetCompletedTodoItemsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        Guid userId = GetUserIdFromClaims();
        var completedItems = await _todoItemService.GetCompletedTodoItemsAsync(userId, page, pageSize);

        if (completedItems is null || !completedItems.Any())
        {
            _logger.LogInformation($"No completed todos for User ID {userId}.");
            return NoContent();
        }

        _logger.LogInformation($"Retrieved completed items successfully for User ID {userId}.");
        return Ok(completedItems);
    }


    [HttpGet("deleted")]
    public async Task<IActionResult> GetDeletedTodoItemsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        Guid userId = GetUserIdFromClaims();
        var result = await _todoItemService.GetDeletedTodoItemsAsync(userId, page, pageSize);

        if (result is null || !result.Any())
        {
            _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nNo deleted todos for User ID {userId} {nameof(GetTodoItemByIdAsync)} request\n------------");
            return NoContent();
        }

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nRetrieved deleted items successfully for User ID {userId} {nameof(GetTodoItemByIdAsync)} request\n------------");

        return Ok(result);
    }


    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoItemByIdAsync(Guid id)
    {
        var result = await _todoItemService.GetTodoItemByIdAsync(id);

        if (result is null)
        {
            _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(GetTodoItemByIdAsync)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemController)}:\nThe method {GetTodoItemByIdAsync} request was successful\n------------");

        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchTodoItemsByCategory([FromQuery] string category)
    {
        Guid userId = GetUserIdFromClaims();
        var result = await _todoItemService.SearchTodoItemsByCategory(userId, category);

        if (result is null)
        {
            _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(SearchTodoItemsByCategory)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemController)}:\nThe method {SearchTodoItemsByCategory} request was successful\n------------");

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTodoAsync([FromBody] TodoItemCreateDto todoDto)
    {
        Guid userId = GetUserIdFromClaims();

        var todoItem = new TodoItem
        {
            UserId = userId,
            Title = todoDto.Title,
            Description = todoDto.Description,
            Category = todoDto.Category,
            CreatedDate = DateTime.UtcNow
        };

        var result = await _todoItemService.AddTodoAsync(todoItem);

        if (result is null)
        {
            _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(AddTodoAsync)} request\n------------");
            return NoContent();
        }

        _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemController)}:\nThe method {AddTodoAsync} request was successful\n------------");
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoAsync(Guid id)
    {
        var result = await _todoItemService.DeleteTodoAsync(id);

        if (result is null)
        {
            _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(DeleteTodoAsync)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemController)}:\nThe method {DeleteTodoAsync} request was successful\n------------");

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoAsync([FromRoute] Guid id, [FromBody] TodoItemUpdateDto todoDto)
    {
        var todoItem = new TodoItem
        {
            Title = todoDto.Title,
            Description = todoDto.Description,
            Category = todoDto.Category,
            IsCompleted = todoDto.IsCompleted,
            IsDeleted = todoDto.IsDeleted
        };

        var result = await _todoItemService.UpdateTodoAsync(id, todoItem);

        if (result is null)
        {
            _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(UpdateTodoAsync)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"\n------------\nMessage from {nameof(TodoItemController)}:\nThe method {UpdateTodoAsync} request was successful\n------------");

        return Ok(result);
    }

    /// <summary>
    /// Retrieves the user's ID from the security claims of the authenticated user. 
    /// It searches for the 'NameIdentifier' claim, which represents the user's unique identifier,
    /// and attempts to parse it into a GUID. If successful, it returns the GUID. If the claim is not found
    /// or cannot be parsed as a GUID, it logs a warning and throws an exception.
    /// </summary>
    /// <returns>The user's ID as a GUID if found and valid.</returns>
    /// <exception cref="Exception">Thrown if the user ID claim is not found or is not a valid GUID.</exception>
    private Guid GetUserIdFromClaims()
    {
        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaim != null && Guid.TryParse(userClaim.Value, out var userId))
        {
            _logger.LogInformation($"Extracted user ID from claims: {userId}");

            _logger.LogInformation($"Extracting user ID from claims: {userClaim.Value}");

            return userId;
        }

        _logger.LogWarning("User ID not found in token claims or is not a valid GUID.");

        throw new Exception("User ID not found in token claims or is not a valid GUID.");
    }
}
