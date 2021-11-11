using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionExtensions.Builders
{
    public class Component
    {
        public Type ServiceType { get; set; } = null!;
        public Type? ImplementationType { get; set; }
        public ServiceLifetime Lifetime { get; set; }

        public static ComponentBuilder For<TService>()
            where TService : class
            => new ComponentBuilder().For<TService>();
        
        public static ComponentBuilder For<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => new ComponentBuilder().For<TService, TImplementation>();
    }
}