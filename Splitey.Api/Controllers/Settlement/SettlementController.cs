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
    
    [HttpGet("/settlements/{id:int}")]
    public async Task<IActionResult> GetList([FromRoute] int id)
    {
        return Ok(await settlementService.Get(id));
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
}