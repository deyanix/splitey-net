using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitey.Api.Models.Transfer;
using Splitey.Core.Services.Settlement.Transfer;

namespace Splitey.Api.Controllers.Settlement;

[Authorize]
public class TransferController(TransferService transferService) : Controller
{
    [HttpGet("/settlements/{id:int}/transfers")]
    public async Task<IActionResult> GetList([FromRoute] int id)
    {
        return Ok(await transferService.GetList(id));
    }
    
    [HttpPost("/settlements/{id:int}/transfers")]
    public async Task<IActionResult> Create([FromRoute] int id, [FromBody] TransferUpdateRequest request)
    {
        return Ok(await transferService.Create(id, request));
    }
    
    [HttpPut("/settlements/{id:int}/transfers/{transferId:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromRoute] int transferId, [FromBody] TransferUpdateRequest request)
    {
        await transferService.Update(id, transferId, request);
        return Ok();
    }
    
    [HttpDelete("/settlements/{id:int}/transfers/{transferId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, [FromRoute] int transferId)
    {
        await transferService.Delete(id, transferId);
        return Ok();
    }
}