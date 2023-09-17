using System.Reflection;
using Splitey.Api.Common.DependencyInjection.Attributes;

namespace Splitey.Api.Common.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        Type singletonType = typeof(SingletonDependencyAttribute);
        
        Assembly
            .GetEntryAssembly()?.DefinedTypes
            .Where(item => item.IsClass)
            .Where(item => item.IsDefined(singletonType, true))
            .ToList()
            .ForEach(item => services.AddSingleton(item.AsType()));
    }
}