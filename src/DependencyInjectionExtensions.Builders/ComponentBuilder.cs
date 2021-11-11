using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionExtensions.Builders
{
    // transient dla Something implementującego ISomething
    // Component.For<ISomething>().ImplementedBy<Something>().Transient();
    // Component.For<ISomething>().ImplementedBy<Something>();
    // Component.For<ISomething, Something>();
    public class ComponentBuilder
    {
        private Type _registeredType = null!;
        private Type? _implementationType;
        private ServiceLifetime _lifeTime = ServiceLifetime.Transient;
        
        public ComponentBuilder For<TService>()
            where TService : class
        {
            _registeredType = typeof(TService);    
            return this;
        }

        public ComponentBuilder ImplementedBy<TImplementation>()
        {
            _implementationType = typeof(TImplementation);
            return this;
        }

        public ComponentBuilder For<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => For<TService>().ImplementedBy<TImplementation>();

        public ComponentBuilder LifetimeStrategy(ServiceLifetime lifetime)
        {
            _lifeTime = lifetime;
            return this;
        }

        public ComponentBuilder Transient() => LifetimeStrategy(ServiceLifetime.Transient);
        public ComponentBuilder Scoped() => LifetimeStrategy(ServiceLifetime.Scoped);

        public ComponentBuilder Singleton() => LifetimeStrategy(ServiceLifetime.Singleton);

        internal Component Build()
        {
            return new Component
            {
                Lifetime = _lifeTime,
                ImplementationType = _implementationType,
                ServiceType = _registeredType
            };
        }
        
        public static implicit operator Component(ComponentBuilder builder)
            => builder.Build();
    }
}