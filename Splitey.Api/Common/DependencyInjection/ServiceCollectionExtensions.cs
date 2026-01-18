using System.Reflection;
using Splitey.Api.Common.DependencyInjection.Attributes;

namespace Splitey.Api.Common.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        GetDependencies(typeof(SingletonDependencyAttribute))
            .ForEach(item => services.AddSingleton(item.AsType()));
        
        GetDependencies(typeof(ScopedDependencyAttribute))
            .ForEach(item => services.AddScoped(item.AsType()));
    }

    private static List<TypeInfo> GetDependencies(Type attributeType)
    {
        return Assembly
            .GetEntryAssembly()?.DefinedTypes
            .Where(item => item.IsClass)
            .Where(item => item.IsDefined(attributeType, true))
            .ToList() ?? [];
    }
}