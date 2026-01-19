using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitey.Data.Repositories.Env.Currency;

namespace Splitey.Api.Controllers.Env;

[Authorize]
public class EnvController(CurrencyRepository currencyRepository) : Controller
{
    [HttpGet("env/currencies")]
    public async Task<IActionResult> Login()
    {
        return Ok(await currencyRepository.GetList());
    }
}