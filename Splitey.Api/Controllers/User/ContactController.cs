using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitey.Api.Models.Contact;
using Splitey.Core.Services.User.Contact;
using Splitey.Models.User.Contact;

namespace Splitey.Api.Controllers.User;

[Authorize]
public class ContactController(ContactService contactService) : Controller
{
    [HttpGet("/contacts")]
    public async Task<IActionResult> GetList()
    {
        return Ok(await contactService.GetList());
    }
    
    [HttpGet("/contacts/{contactId:int}")]
    public async Task<IActionResult> Get([FromRoute] int contactId)
    {
        return Ok(await contactService.Get(contactId));
    }
    
    [HttpPost("/contacts/{contactId:int}")]
    public async Task<IActionResult> Create([FromRoute] int contactId, [FromBody] ContactUpdate request)
    {
        return Ok(await contactService.Create(request));
    }
    
    [HttpPut("/contacts/{contactId:int}")]
    public async Task<IActionResult> Update([FromRoute] int contactId, [FromBody] ContactUpdate request)
    {
        await contactService.Update(contactId, request);
        return Ok();
    }
    
    [HttpDelete("/contacts/{contactId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int contactId)
    {
        await contactService.Delete(contactId);
        return Ok();
    }
    
    [HttpPut("/contacts/{contactId:int}/access")]
    public async Task<IActionResult> UpsertAccess([FromRoute] int contactId, [FromBody] ContactUpdateAccess request)
    {
        await contactService.UpsertAccess(contactId, request);
        return Ok();
    }
    
    [HttpDelete("/contacts/{contactId:int}/access")]
    public async Task<IActionResult> DeleteAccess([FromRoute] int contactId, [FromBody] ContactDeleteAccess request)
    {
        await contactService.DeleteAccess(contactId, request);
        return Ok();
    }
}