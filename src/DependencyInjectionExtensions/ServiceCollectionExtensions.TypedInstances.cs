using System;
using DependencyInjectionExtensions.Factories.Typed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DependencyInjectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTransient<TInterface, TImplementation>(
            this IServiceCollection services, 
            Type triggerType, 
            Func<object, object[]> getArgs)
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