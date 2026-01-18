using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitey.Core.Services.Settlement.Settlement;
using Splitey.Models.Settlement.Settlement;

namespace Splitey.Api.Controllers.Settlement;

[Authorize]
public class SettlementController(SettlementService settlementService) : Controller
{
    [HttpGet("/settlements")]
    public async Task<IActionResult> GetList()
    {
        return Ok(await settlementService.GetList());
    }
    
    [HttpPost("/settlements")]
    public async Task<IActionResult> Create([FromBody] SettlementUpdate request)
    {
        return Ok(await settlementService.Create(request));
    }
    
    [HttpPut("/settlements/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SettlementUpdate request)
    {
        await settlementService.Update(id, request);
        return Ok();
    }
    
    [HttpDelete("/settlements/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await settlementService.Delete(id);
        return Ok();
    }
    
    [HttpGet("/settlements/{id:int}/members")]
    public async Task<IActionResult> GetMembers([FromRoute] int id)
    {
        return Ok(await settlementService.GetMembers(id));
    }
    
    [HttpGet("/settlements/{id:int}/arrangement")]
    public async Task<IActionResult> GetArrangement([FromRoute] int id, [FromQuery] bool optimized)
    {
        return Ok(await settlementService.GetArrangement(id, optimized));
    }

    [HttpGet("/settlements/{id:int}/summary")]
    public async Task<IActionResult> GetSummary([FromRoute] int id)
    {
        return Ok(await settlementService.GetSummary(id));
    }
}