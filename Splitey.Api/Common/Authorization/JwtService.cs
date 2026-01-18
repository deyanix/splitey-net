using System.Security.Cryptography;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using Newtonsoft.Json;
using Splitey.Api.Common.DependencyInjection.Attributes;
using Splitey.Api.Models.User.User;

namespace Splitey.Api.Common.Authorization;

[SingletonDependency]
public class JwtService
{
    private readonly RSA _rsa;
    private readonly IJwtAlgorithm _algorithm;
    
    public JwtService()
    {
        _rsa = RSA.Create();
        _rsa.ImportFromPem(File.ReadAllText("keys/Default.key"));

        _algorithm = new RS2048Algorithm(_rsa, _rsa);
    }

    public string GenerateToken(UserModel user)
    {
        return JwtBuilder.Create()
            .WithAlgorithm(_algorithm)
            .AddClaim("sub", user.Id)
            .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(8).ToUnixTimeSeconds())
            .AddClaim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            .Encode();
    }

    public int? Validate(string token)
    {
        try
        {
            string payloadJson = JwtBuilder.Create()
                .WithAlgorithm(_algorithm)
                .Decode(token);
            var payload = JsonConvert.DeserializeObject<IDictionary<string, object>>(payloadJson);

            object? userId = payload?["sub"];
            if (userId is int or long)
            {
                return Convert.ToInt32(userId);
            }
        }
        catch (Exception)
        {
            // ignored
        }

        return null;  
    }
}