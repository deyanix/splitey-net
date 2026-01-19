using Microsoft.Extensions.DependencyInjection;
using Splitey.Data.Sql;
using Splitey.DependencyInjection;

namespace Splitey.Data.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpliteyData(this IServiceCollection services, string connectionString)
    {
        return services
            .AddSingleton<ISqlConnectionFactory>(_ => 
                new SqlConnectionFactory(connectionString))
            .AddDependencies(typeof(ServiceCollectionExtensions).Assembly);
    }
}