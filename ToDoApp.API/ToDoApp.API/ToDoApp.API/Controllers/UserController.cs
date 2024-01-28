using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp.API.Data.Entities;
using ToDoApp.API.Models.Requests;
using ToDoApp.API.Services;
using ToDoApp.API.Services.Interfaces;

namespace ToDoApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(
        IUserService userService,
        ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (loginRequest is null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest();
        }

        var user = new User
        {
            Username = loginRequest.Username,
            Password = loginRequest.Password,
        };

        var result = await _userService.GetUserAsync(user);

        if (result is null)
        {
            _logger.LogInformation($"\n------------\nMessage from {nameof(UserController)}:\nError while processing {nameof(Login)} request\n------------");

            return NoContent();
        }

        _logger.LogInformation($"\n------------\nMessage from {nameof(UserController)}:\nThe method {Login} request was successful\n------------");

        return Ok(new
        {
            Token = result.Token,
            Message = $"{result}"
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] SignupRequest signupRequest)
    {
        if (signupRequest is null)
        {
            return BadRequest();
        }

        var user = new User
        {
            FirstName = signupRequest.FirstName,
            SecondName = signupRequest.SecondName,
            Username = signupRequest.Username,
            Password = signupRequest.Password,
            Email = signupRequest.Email,
            Token = string.Empty
        };

        var addedUser = await _userService.AddUserAsync(user);

        if (addedUser is null)
        {
            _logger.LogInformation("Error while processing Register request");
            return NoContent();
        }

        addedUser.Token = CreateJwt(addedUser);

        await _userService.UpdateUserAsync(addedUser);

        _logger.LogInformation("The method Register request was successful");

        return Ok(new
        {
            Token = addedUser.Token,
            Message = "Registration successful"
        });
    }

    /// <summary>
    /// Generates a JWT for a specified user. The token includes claims for the user's name and identifier.
    /// It uses HMAC SHA256 for signing the token with a predefined secret key. The generated token is valid for 10 days from
    /// the creation time.
    /// </summary>
    /// <param name="user">The user for whom the JWT is being created.</param>
    /// <returns>A string representation of the JWT.</returns>
    private string CreateJwt(User user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes("superpuperduperlongandthemostsecretattheworld");

        var identity = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.SecondName}"),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        });

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddDays(10),
            SigningCredentials = credentials
        };

        var token = jwtTokenHandler.CreateToken(tokenDescription);

        _logger.LogInformation($"Creating JWT for User ID: {user.Id}");

        return jwtTokenHandler.WriteToken(token);
    }
}
