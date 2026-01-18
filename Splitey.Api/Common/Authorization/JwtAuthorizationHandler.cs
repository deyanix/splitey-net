using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Splitey.Api.Models.User.User;
using Splitey.Api.Services.User.User;

namespace Splitey.Api.Common.Authorization;

public class JwtAuthorizationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IHttpContextAccessor httpContextAccessor,
        JwtService jwtService,
        UserService userService)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? header = httpContextAccessor.HttpContext?.Request.Headers.Authorization.FirstOrDefault();
        if (!string.IsNullOrEmpty(header))
        {
            int? userId = jwtService.Validate(header);
            if (userId.HasValue)
            {
                UserModel? user = await userService.Get(userId.Value);
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    };
                    var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
                    AuthenticationTicket ticket = new(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }
            }
        }
        
        return AuthenticateResult.Fail("Authorization failed");
    }
}