using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitey.Api.Services.Payment.Settlement;

namespace Splitey.Api.Controllers.Payment;

[Authorize]
public class SettlementController(SettlementService settlementService) : Controller
{
    [HttpGet("/settlements/{id}/arrangement")]
    public async Task<IActionResult> GetArrangement([FromRoute] int id, [FromQuery] bool optimized)
    {
        return Ok(await settlementService.GetArrangement(id, optimized));
    }

    [HttpGet("/settlements/{id}/summary")]
    public async Task<IActionResult> GetSummary([FromRoute] int id)
    {
        return Ok(await settlementService.GetSummary(id));
    }
}