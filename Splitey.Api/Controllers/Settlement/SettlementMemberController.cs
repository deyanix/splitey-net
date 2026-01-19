using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitey.Api.Models.SettlementMember;
using Splitey.Core.Services.Settlement.SettlementMember;
using Splitey.Models.Settlement.SettlementMember;

namespace Splitey.Api.Controllers.Settlement;

[Authorize]
public class SettlementMemberController(SettlementMemberService settlementMemberService) : Controller
{
    [HttpGet("/settlements/{id:int}/members")]
    public async Task<IActionResult> GetList([FromRoute] int id)
    {
        return Ok(await settlementMemberService.GetList(id));
    }
    
    [HttpPost("/settlements/{id:int}/members/users")]
    public async Task<IActionResult> UpsertUser([FromRoute] int id, [FromBody] SettlementMemberUpdateUser request)
    {
        await settlementMemberService.UpsertUser(id, request);
        return Ok();
    }
    
    [HttpDelete("/settlements/{id:int}/members/users")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id, [FromBody] SettlementMemberDeleteUser request)
    {
        await settlementMemberService.DeleteUser(id, request);
        return Ok();
    }
    
    [HttpPost("/settlements/{id:int}/members/contacts")]
    public async Task<IActionResult> UpsertContact([FromRoute] int id, [FromBody] SettlementMemberUpdateContact request)
    {
        await settlementMemberService.UpsertContact(id, request);
        return Ok();
    }
    
    [HttpDelete("/settlements/{id:int}/members/contacts")]
    public async Task<IActionResult> DeleteContact([FromRoute] int id, [FromBody] SettlementMemberUpdateContact request)
    {
        await settlementMemberService.DeleteContact(id, request);
        return Ok();
    }
}