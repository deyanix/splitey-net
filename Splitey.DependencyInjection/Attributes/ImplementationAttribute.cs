using Microsoft.Extensions.DependencyInjection;

namespace Splitey.DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public abstract class ImplementationAttribute(ServiceLifetime lifetime) : Attribute
{
    public Type? Type { get; init; }
    public ServiceLifetime Lifetime { get; } = lifetime;
}