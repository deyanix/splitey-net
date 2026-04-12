using Microsoft.Extensions.DependencyInjection;

namespace Splitey.DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Interface)]
public sealed class DependencyAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; init; } = ServiceLifetime.Transient;
}