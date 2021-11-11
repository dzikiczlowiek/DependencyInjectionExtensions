using System;
using DependencyInjectionExtensions.Tests.TestsComponents;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DependencyInjectionExtensions.Builders.Tests
{
    public class ComponentsTestItem
    {
        public ComponentsTestItem(Component component, Type serviceType, Type? implementationType, ServiceLifetime lifetime)
        {
            Component = component;
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }
        
        public Component Component { get; }
        public Type ServiceType { get; }
        public Type? ImplementationType { get; }
        public ServiceLifetime Lifetime { get; }

        public static ComponentsTestItem CreateFor<TService>(Component component, ServiceLifetime lifetime)
            => new ComponentsTestItem(component, typeof(TService), typeof(TService), lifetime);
        
        public static ComponentsTestItem CreateFor<TService, TImplementation>(Component component, ServiceLifetime lifetime)
            => new ComponentsTestItem(component, typeof(TService), typeof(TImplementation), lifetime);

        public static TheoryData<ComponentsTestItem> TransientRegistrationTestItemsSource()
        {
            var theory = new TheoryData<ComponentsTestItem>();
            theory.Add(CreateFor<IFoo, KungFoo>(Component.For<IFoo, KungFoo>(), ServiceLifetime.Transient));
            theory.Add(CreateFor<IFoo, KungFoo>(Component.For<IFoo>().ImplementedBy<KungFoo>(),
                ServiceLifetime.Transient));
            theory.Add(CreateFor<IFoo, KungFoo>(Component.For<IFoo>().ImplementedBy<KungFoo>().Transient(),
                ServiceLifetime.Transient));
            theory.Add(CreateFor<KungFoo>(Component.For<KungFoo>(), ServiceLifetime.Transient));
            theory.Add(CreateFor<KungFoo>(Component.For<KungFoo>().Transient(), ServiceLifetime.Transient));
            return theory;
        }

        public static TheoryData<ComponentsTestItem> ScopedRegistrationTestItemsSource()
        {
            var theory = new TheoryData<ComponentsTestItem>();
            theory.Add(CreateFor<IFoo, KungFoo>(Component.For<IFoo>().ImplementedBy<KungFoo>().Scoped(), ServiceLifetime.Scoped));
            theory.Add(CreateFor<KungFoo>(Component.For<KungFoo>().Scoped(), ServiceLifetime.Scoped));
            return theory;
        }
        
        public static TheoryData<ComponentsTestItem> SingletonRegistrationTestItemsSource()
        {
            var theory = new TheoryData<ComponentsTestItem>();
            theory.Add(CreateFor<IFoo, KungFoo>(Component.For<IFoo>().ImplementedBy<KungFoo>().Singleton(), ServiceLifetime.Singleton));
            theory.Add(CreateFor<KungFoo>(Component.For<KungFoo>().Singleton(), ServiceLifetime.Singleton));
            return theory;
        }

        public override string ToString()
        {
            if (Component.ImplementationType == null)
            {
                return $"{Component.ServiceType.Name} -> {Component.Lifetime}";
            }

            return $"{Component.ServiceType.Name} -> {Component.ImplementationType.Name} -> {Component.Lifetime}";
        }
    }
}