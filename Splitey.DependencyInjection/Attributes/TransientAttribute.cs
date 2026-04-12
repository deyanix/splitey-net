using Microsoft.Extensions.DependencyInjection;

namespace Splitey.DependencyInjection.Attributes;

public class TransientAttribute() : ImplementationAttribute(ServiceLifetime.Transient);