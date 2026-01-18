using Microsoft.AspNetCore.Authentication;
using Splitey.Authorization.Handler;

namespace Splitey.Authorization;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddJwt(this AuthenticationBuilder builder)
    {
        return builder
            .AddScheme<AuthenticationSchemeOptions, JwtAuthorizationHandler>("JWT", options => { });
    }
}