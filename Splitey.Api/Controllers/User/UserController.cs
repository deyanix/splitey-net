using Microsoft.AspNetCore.Mvc;
using Splitey.Core.Services.User.User;
using Splitey.Models.User.User;

namespace Splitey.Api.Controllers.User;

public class UserController(UserService userService) : Controller
{
    [HttpPost("users/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest data)
    {
        return Ok(await userService.Login(data));
    }
}