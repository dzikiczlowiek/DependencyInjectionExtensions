using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTransient<TInterface, TImplementation>(this IServiceCollection services,
            params Depends[] depends)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            var implementationType = typeof(TImplementation);
            var ctor = implementationType.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                .SingleOrDefault();

            if (ctor == null)
            {
                throw new Exception($"There is no one public constructor for '{implementationType.Name}'");
            }

            services.AddTransient<TInterface>(sp =>
            {
                var arguments = ctor.GetParameters()
                    .Select(p => ResolveInstance(sp, p.ParameterType, depends)).ToArray();

                var instance = (TInterface)ActivatorUtilities.CreateInstance<TImplementation>(sp, arguments);
                return instance;
            });
            
            return services;

            object ResolveInstance(IServiceProvider sp, Type serviceType, Depends[] dependents)
            {
                var d = dependents.SingleOrDefault(x => x.ServiceType == serviceType);
                if (d != null)
                {
                    var searchedType = serviceType;
                    if (d.Named != null)
                    {
                        return sp.GetRequiredService(serviceType, d.Named);
                    }
                }

                return sp.GetRequiredService(serviceType);
            }
        }

    }
}