using Microsoft.AspNetCore.Mvc;
using Splitey.Api.Models.User;
using Splitey.Core.Services.User.User;
using Splitey.Models.User.User;

namespace Splitey.Api.Controllers.User;

public class UserController(UserService userService) : Controller
{
    [HttpPost("users/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest data)
    {
        await userService.Login(data);
        return Ok();
    }
    
    [HttpPost("users/logout")]
    public async Task<IActionResult> Logout()
    {
        await userService.Logout();
        return Ok();
    }
}