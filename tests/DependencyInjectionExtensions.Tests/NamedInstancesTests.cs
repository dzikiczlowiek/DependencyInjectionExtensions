using System;
using DependencyInjectionExtensions.Tests.TestsComponents;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DependencyInjectionExtensions.Tests
{
    public class NamedInstancesTests : TestsBase
    {
        [Theory]
        [InlineData(typeof(BooFoo), "Boo")]
        [InlineData(typeof(KungFoo), "Kung")]
        public void WhenResolvingServiceWithNameShouldReturnThatNamedService(Type fooType, string name)
        {
            var serviceProvider = ConfigureProvider(services =>
            {
                services.AddTransient<IFoo, BooFoo>("Boo");
                services.AddTransient<IFoo, KungFoo>("Kung");
            });

            var instance = serviceProvider.GetRequiredService<IFoo>(name);

            instance.Should().BeOfType(fooType);
        }
        
        [Fact]
        public void WhenResolvedInstanceHaveNamedDependencyShouldUseThatDependency()
        {
            var serviceProvider = ConfigureProvider(services =>
            {
                services.AddTransient<IFoo, BooFoo>("Boo");
                services.AddTransient<IFoo, KungFoo>("Kung");
                services.AddTransient<IScooby, ScoobyDoo>();
                services.AddTransient<IBar, BarService>(typeof(IFoo).DependsOn(typeof(KungFoo), "Boo"));
            });

            var instance = (BarService)serviceProvider.GetRequiredService<IBar>();
            
            instance.Foo.Should().BeOfType(typeof(BooFoo));
        }
    }
}