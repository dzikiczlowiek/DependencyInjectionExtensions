using DependencyInjectionExtensions.Tests.Core;
using FluentAssertions;
using Xunit;

namespace DependencyInjectionExtensions.Builders.Tests
{
    public class RegisterTests : TestsBase
    {
        [Theory]
        [MemberData(nameof(ComponentsTestItem.TransientRegistrationTestItemsSource), MemberType = typeof(ComponentsTestItem))]
        [MemberData(nameof(ComponentsTestItem.ScopedRegistrationTestItemsSource), MemberType = typeof(ComponentsTestItem))]
        [MemberData(nameof(ComponentsTestItem.SingletonRegistrationTestItemsSource), MemberType = typeof(ComponentsTestItem))]
        public void RegisteringComponentsWithImplementationShouldCreatedServiceDescriptor(ComponentsTestItem testItem)
        {
            ConfigureProvider(services =>
            {
                var builder = new ContainerBuilder(services);
                builder
                    .Register(testItem.Component);
            });

            Services.Should()
                .ContainSingle(descriptor =>
                    descriptor.ServiceType == testItem.ServiceType && descriptor.ImplementationType == testItem.ImplementationType && descriptor.Lifetime == testItem.Lifetime)
                .And
                .HaveCount(1);
        }
    }
}