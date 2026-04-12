using Microsoft.Extensions.DependencyInjection;

namespace Splitey.DependencyInjection.Attributes;

public sealed class SingletonAttribute() : ImplementationAttribute(ServiceLifetime.Singleton);