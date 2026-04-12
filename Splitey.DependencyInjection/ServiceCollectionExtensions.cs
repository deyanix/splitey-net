using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Splitey.DependencyInjection.Attributes;

namespace Splitey.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new 
            { 
                Implementation = t, 
                Attributes = t.GetCustomAttributes<ImplementationAttribute>(false) 
            })
            .Where(t => t.Attributes.Any());
        
        foreach (var typeInfo in types)
        {
            foreach (var attr in typeInfo.Attributes)
            {
                var serviceType = attr.Type ?? GetDefaultInterface(typeInfo.Implementation);
                var lifetime = MapLifetime(attr);
                
                services.Add(new ServiceDescriptor(serviceType, typeInfo.Implementation, lifetime));
            }
        }

        return services;
    }
    
    private static Type GetDefaultInterface(Type implementation)
    {
        var interfaces = implementation.GetInterfaces();
        if (interfaces.Length == 0) 
            return implementation;
        
        var nameMatch = interfaces.FirstOrDefault(i => i.Name == $"I{implementation.Name}");
        return nameMatch ?? interfaces[0];
    }
    
    private static ServiceLifetime MapLifetime(ImplementationAttribute attr) => attr switch
    {
        SingletonAttribute => ServiceLifetime.Singleton,
        ScopedAttribute => ServiceLifetime.Scoped,
        _ => ServiceLifetime.Transient,
    };
}