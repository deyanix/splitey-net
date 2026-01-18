using Microsoft.Extensions.DependencyInjection;
using Splitey.DependencyInjection;

namespace Splitey.Data.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpliteyData(this IServiceCollection services)
    {
        return services.AddDependencies(typeof(ServiceCollectionExtensions).Assembly);
    }
}