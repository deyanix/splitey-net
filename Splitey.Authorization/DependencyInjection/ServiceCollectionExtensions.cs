using Microsoft.Extensions.DependencyInjection;
using Splitey.DependencyInjection;

namespace Splitey.Authorization.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpliteyAuthorization(this IServiceCollection services)
    {
        return services.AddDependencies(typeof(ServiceCollectionExtensions).Assembly);
    }
}