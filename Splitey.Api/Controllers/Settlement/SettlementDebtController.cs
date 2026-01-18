using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitey.Core.Services.Settlement.Settlement;

namespace Splitey.Api.Controllers.Settlement;

[Authorize]
public class SettlementDebtController(SettlementDebtService settlementDebtService) : Controller
{
    [HttpGet("/settlements/{id:int}/debts")]
    public async Task<IActionResult> GetDebts([FromRoute] int id)
    {
        return Ok(await settlementDebtService.GetDebts(id));
    }

    [HttpGet("/settlements/{id:int}/debts/simplified")]
    public async Task<IActionResult> GetSimplifiedDebts([FromRoute] int id)
    {
        return Ok(await settlementDebtService.GetSimplifiedDebts(id));
    }

    [HttpGet("/settlements/{id:int}/summary")]
    public async Task<IActionResult> GetSummary([FromRoute] int id)
    {
        return Ok(await settlementDebtService.GetSummary(id));
    }
}