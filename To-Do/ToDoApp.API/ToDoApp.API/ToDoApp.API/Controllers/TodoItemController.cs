using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.API.Data.Entities;
using ToDoApp.API.Services.Interfaces;

namespace ToDoApp.API.Controllers;

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
        var result = await _todoItemService.GetTodoItemsAsync(page, pageSize);

        if (result is null)
        {
            _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(GetTodoItemsAsync)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nThe method {GetTodoItemsAsync} request was successful\n------------");

        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoItemByIdAsync(Guid id)
    {
        var result = await _todoItemService.GetTodoItemByIdAsync(id);

        if (result is null)
        {
            _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(GetTodoItemByIdAsync)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nThe method {GetTodoItemByIdAsync} request was successful\n------------");

        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchTodoItemsByCategory([FromQuery] string category)
    {
        var result = await _todoItemService.SearchTodoItemsByCategory(category);

        if (result is null)
        {
            _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(SearchTodoItemsByCategory)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nThe method {SearchTodoItemsByCategory} request was successful\n------------");

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTodoAsync([FromBody] TodoItem todo)
    {
        var result = await _todoItemService.AddTodoAsync(todo);

        if (result is null)
        {
            _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(AddTodoAsync)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nThe method {AddTodoAsync} request was successful\n------------");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoAsync(Guid id)
    {
        var result = await _todoItemService.DeleteTodoAsync(id);

        if (result is null)
        {
            _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(DeleteTodoAsync)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nThe method {DeleteTodoAsync} request was successful\n------------");

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoAsync([FromRoute] Guid id, [FromBody] TodoItem todo)
    {
        var result = await _todoItemService.UpdateTodoAsync(id, todo);

        if (result is null)
        {
            _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nError while processing {nameof(UpdateTodoAsync)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"------------\nMessage from {nameof(TodoItemController)}:\nThe method {UpdateTodoAsync} request was successful\n------------");

        return Ok(result);
    }
}
