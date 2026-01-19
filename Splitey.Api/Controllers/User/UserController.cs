using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitey.Api.Models.User;
using Splitey.Core.Services.User.User;

namespace Splitey.Api.Controllers.User;

public class UserController(UserService userService) : Controller
{
    [HttpPost("users/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest data)
    {
        return Ok(await userService.Login(data));
    }
    
    [Authorize]
    [HttpPost("users/logout")]
    public async Task<IActionResult> Logout()
    {
        await userService.Logout();
        return Ok();
    }
    
    [Authorize]
    [HttpGet("users/current")]
    public async Task<IActionResult> Get()
    {
        return Ok(await userService.GetCurrent());
    }
}