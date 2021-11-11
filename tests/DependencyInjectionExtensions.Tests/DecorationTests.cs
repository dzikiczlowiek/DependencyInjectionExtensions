using DependencyInjectionExtensions.Tests.TestsComponents;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DependencyInjectionExtensions.Tests
{
    public sealed class DecorationTests : TestsBase
    {
        [Fact]
        public void T1()
        {
            var serviceProvider = ConfigureProvider(services =>
            {
                services.AddTransient<IFoo, BooFoo>();
                services.Decorate<IFoo, FooDecorator>();
            });

            var instance = serviceProvider.GetRequiredService<IFoo>();

            instance.Should().BeOfType<FooDecorator>();
            ((FooDecorator) instance).Inner.Should().BeOfType<BooFoo>();
        }
    }
}