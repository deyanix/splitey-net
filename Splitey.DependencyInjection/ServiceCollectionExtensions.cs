using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Splitey.DependencyInjection.Attributes;

namespace Splitey.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, Assembly assembly)
    {
        GetDependencies(assembly, typeof(SingletonDependencyAttribute))
            .ForEach(item => services.AddSingleton(item));
        
        GetDependencies(assembly, typeof(ScopedDependencyAttribute))
            .ForEach(item => services.AddScoped(item));

        return services;
    }

    private static List<Type> GetDependencies(Assembly assembly, Type attributeType)
    {
        return assembly.GetTypes()
            .Where(item => item.IsClass && !item.IsAbstract)
            .Where(item => item.IsDefined(attributeType, true))
            .ToList() ?? [];
    }
}