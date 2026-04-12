using Microsoft.Extensions.DependencyInjection;

namespace Splitey.DependencyInjection.Attributes;

public sealed class ScopedAttribute() : ImplementationAttribute(ServiceLifetime.Scoped);