using Microsoft.AspNetCore.Mvc;
using Splitey.Api.Models.User.User;
using Splitey.Api.Services.User.User;

namespace Splitey.Api.Controllers.User;

public class UserController(UserService userService) : Controller
{
    [HttpPost("users/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest data)
    {
        return Ok(await userService.Login(data));
    }
}