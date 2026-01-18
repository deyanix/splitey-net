using Microsoft.AspNetCore.Authentication;

namespace Splitey.Api.Common.Authorization;

public static class JwtAuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddJwt(this AuthenticationBuilder builder)
    {
        return builder
            .AddScheme<AuthenticationSchemeOptions, JwtAuthorizationHandler>("JWT", options => { });
    }
}