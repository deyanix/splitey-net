using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Authorization;

namespace Splitey.Authorization;

[ScopedDependency]
public class AuthorizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IUserContext User => GetCurrentUser() 
        ?? throw new Exception("User is not authenticated");

    public IUserContext? GetCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            return null;
            
        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idClaim == null)
            return null;
            
        var nameClaim = user.FindFirst(ClaimTypes.Name)?.Value;
        if (nameClaim == null)
            return null;

        if (!int.TryParse(idClaim, out var id))
            return null;
            
        return new UserContext()
        {
            Id = id,
            Username = nameClaim,
        };
    }
    
    public async Task SignIn(IUserContext user, bool rememberMe)
    {
        if (_httpContextAccessor.HttpContext == null)
            return;
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = rememberMe,
        };
        
        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            authProperties);
    }
    
    public async Task SignOut()
    {
        if (_httpContextAccessor.HttpContext == null)
            return;
        
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}