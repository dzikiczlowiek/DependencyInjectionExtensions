using System;
using System.ComponentModel;
using DependencyInjectionExtensions.Tests.Core;
using DependencyInjectionExtensions.Tests.TestsComponents;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DependencyInjectionExtensions.Builders.Tests
{
    public class RegisterTests : TestsBase
    {
        [Theory]
        [MemberData(nameof(ComponentsTestItem.TransientRegistrationTestItemsSource), MemberType = typeof(ComponentsTestItem))]
        [MemberData(nameof(ComponentsTestItem.ScopedRegistrationTestItemsSource), MemberType = typeof(ComponentsTestItem))]
        [MemberData(nameof(ComponentsTestItem.SingletonRegistrationTestItemsSource), MemberType = typeof(ComponentsTestItem))]
        public void RegisteringComponentsShouldCreatedServiceDescriptor(ComponentsTestItem testItem)
        {
            ConfigureProvider(services =>
            {
                var builder = new ContainerBuilder(services);
                builder
                    .Register(testItem.Component);
            });

            Services.Should()
                .ContainSingle(descriptor =>
                    descriptor.ServiceType == testItem.ServiceType && descriptor.Lifetime == testItem.Lifetime)
                .And
                .HaveCount(1);
        }
    }

    public class ComponentsTestItem
    {
        public ComponentsTestItem(Component component, Type serviceType, ServiceLifetime lifetime)
        {
            Component = component;
            ServiceType = serviceType;
            Lifetime = lifetime;
        }
        
        public Component Component { get; }
        public Type ServiceType { get; }
        public ServiceLifetime Lifetime { get; }

        public static ComponentsTestItem Create<T>(Component component, ServiceLifetime lifetime)
            => new ComponentsTestItem(component, typeof(T), lifetime);

        public static ComponentsTestItem CreateTransient<T>(Component component) =>
            Create<T>(component, ServiceLifetime.Transient);
        
        public static ComponentsTestItem CreateScoped<T>(Component component) =>
            Create<T>(component, ServiceLifetime.Scoped);
        
        public static ComponentsTestItem SingletonScoped<T>(Component component) =>
            Create<T>(component, ServiceLifetime.Singleton);
            
        
        public static TheoryData<ComponentsTestItem> TransientRegistrationTestItemsSource()
        {
            var theory = new TheoryData<ComponentsTestItem>();
            theory.Add(CreateTransient<IFoo>(Component.For<IFoo, KungFoo>()));
            theory.Add(CreateTransient<IFoo>(Component.For<IFoo>().ImplementedBy<KungFoo>()));
            theory.Add(CreateTransient<IFoo>(Component.For<IFoo>().ImplementedBy<KungFoo>().Transient()));
            theory.Add(CreateTransient<KungFoo>(Component.For<KungFoo>()));
            theory.Add(CreateTransient<KungFoo>(Component.For<KungFoo>().Transient()));
            return theory;
        }
        
        public static TheoryData<ComponentsTestItem> ScopedRegistrationTestItemsSource()
        {
            var theory = new TheoryData<ComponentsTestItem>();
            theory.Add(CreateScoped<IFoo>(Component.For<IFoo>().ImplementedBy<KungFoo>().Scoped()));
            theory.Add(CreateScoped<KungFoo>(Component.For<KungFoo>().Scoped()));
            return theory;
        }
        
        public static TheoryData<ComponentsTestItem> SingletonRegistrationTestItemsSource()
        {
            var theory = new TheoryData<ComponentsTestItem>();
            theory.Add(SingletonScoped<IFoo>(Component.For<IFoo>().ImplementedBy<KungFoo>().Singleton()));
            theory.Add(SingletonScoped<KungFoo>(Component.For<KungFoo>().Singleton()));
            return theory;
        }
    }
}