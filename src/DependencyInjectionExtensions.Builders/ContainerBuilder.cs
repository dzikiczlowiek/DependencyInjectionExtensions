using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionExtensions.Builders
{
    public class ContainerBuilder
    {
        private readonly IServiceCollection _services;

        public ContainerBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public ContainerBuilder Register(Component component)
        {
            ServiceDescriptor? serviceDescriptor = null;
            if (component.ImplementationType == null)
            {
                serviceDescriptor = new ServiceDescriptor(component.ServiceType, component.ServiceType,
                    component.Lifetime);
            }
            else
            {
                serviceDescriptor = new ServiceDescriptor(component.ServiceType, component.ImplementationType,
                    component.Lifetime);
            }
            _services.Add(serviceDescriptor);
            return this;
        }
    }
}