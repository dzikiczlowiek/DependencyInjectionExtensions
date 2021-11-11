using System;
using System.Linq;
using System.Reflection;
using DependencyInjectionExtensions.Factories.Named;
using DependencyInjectionExtensions.Factories.Typed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DependencyInjectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTransient<TInterface, TImplementation>(this IServiceCollection services, string named)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            services.TryAddSingleton<INamedInstanceFactory<TInterface>, NamedInstanceFactory<TInterface>>();
            services.AddTransient<TImplementation>();
            var selector = new NamedInstanceSelector<TInterface>(named, typeof(TImplementation));
            services.AddTransient<INamedInstanceSelector<TInterface>>(_ => selector);
            return services;
        }
        
        public static IServiceCollection AddTransient<TInterface>(this IServiceCollection services, Type implementationType, string named)
            where TInterface : class
        {
            services.TryAddSingleton<INamedInstanceFactory<TInterface>, NamedInstanceFactory<TInterface>>();
            services.AddTransient(implementationType);
            var selector = new NamedInstanceSelector<TInterface>(named, implementationType);
            services.AddTransient<INamedInstanceSelector<TInterface>>(_ => selector);
            return services;
        }

        public static TInterface GetRequiredService<TInterface>(this IServiceProvider serviceProvider, string name)
            where TInterface : class
        {
            var namedFactory = serviceProvider.GetRequiredService<INamedInstanceFactory<TInterface>>();
            return namedFactory.Resolve(name);
        }
        
        public static TInterface GetRequiredService<TInterface>(this IServiceProvider serviceProvider, object parameter)
            where TInterface : class
        {
            var namedFactory = serviceProvider.GetRequiredService<ITypedInstanceFactory<TInterface>>();
            return namedFactory.Resolve(parameter.GetType(), parameter);
        }
        
        public static object GetRequiredService(this IServiceProvider serviceProvider, Type serviceType, string name)
        {
            var namedInstanceFactoryType = (typeof(INamedInstanceFactory<>)).MakeGenericType(serviceType);
            var namedFactory = (INamedInstanceFactory)serviceProvider.GetRequiredService(namedInstanceFactoryType);
            return namedFactory.Resolve(name);
        }
        
        public static IServiceCollection AddTransient<TInterface, TImplementation>(this IServiceCollection services, Type triggerType, Func<object, object[]> getArgs)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            services.TryAddSingleton<ITypedInstanceFactory<TInterface>, TypedInstanceFactory<TInterface>>();
            services.AddTransient<TImplementation>();
            var selector = new TypedInstanceSelector<TInterface>( typeof(TImplementation), triggerType, getArgs);
            services.AddTransient<ITypedInstanceSelector<TInterface>>(_ => selector);
            return services;
        }
    }
}