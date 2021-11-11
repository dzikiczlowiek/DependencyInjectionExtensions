using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection Decorate(
            this IServiceCollection services, 
            Type serviceType,
            Type decoratorType)
        {
            var descriptors = GetDescriptors(services, serviceType);
            foreach(var descriptor in descriptors)
            {
                var index = services.IndexOf(descriptor);
                Func<IServiceProvider, object> factory = 
                    sp => 
                        ActivatorUtilities.CreateInstance(
                            sp, 
                            decoratorType, 
                            CreateInstance(sp, descriptor));
                var sd =  ServiceDescriptor.Describe(descriptor.ServiceType, factory, descriptor.Lifetime);
                services[index] = sd;
            }
		
            return services;
        }

        public static IServiceCollection Decorate<TService, TDecorator>(this IServiceCollection services)
            where TService : class
            where TDecorator : class, TService
        {
            return services.Decorate(typeof(TService), typeof(TDecorator));
        }
        
        private static object CreateInstance(IServiceProvider serviceProvider, ServiceDescriptor serviceDescriptor)
        {
            if (serviceDescriptor.ImplementationInstance != null)
            {
                return serviceDescriptor.ImplementationInstance;
            }

            if (serviceDescriptor.ImplementationType != null)
            {
                return ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider,
                    serviceDescriptor.ImplementationType);
            }

            return serviceDescriptor.ImplementationFactory?.Invoke(serviceProvider)!;
        }

        private static ICollection<ServiceDescriptor> GetDescriptors(this IServiceCollection services, Type serviceType)
        {
            return services.Where(service => service.ServiceType == serviceType).ToArray();
        }
    }
}