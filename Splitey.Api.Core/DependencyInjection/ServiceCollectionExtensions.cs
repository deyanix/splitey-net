using Microsoft.Extensions.DependencyInjection;
using Splitey.DependencyInjection;

namespace Splitey.Core.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpliteyApiCore(this IServiceCollection services)
    {
        return services.AddDependencies(typeof(ServiceCollectionExtensions).Assembly);
    }
}