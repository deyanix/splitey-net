using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Splitey.DependencyInjection.Attributes;

namespace Splitey.Authorization;

[ScopedDependency]
public class AuthorizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId
    {
        get 
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.TryParse(idClaim, out var id) ? 
                id : throw new Exception("Unknown a user");
        }
    }
}