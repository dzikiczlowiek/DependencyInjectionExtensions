using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionExtensions.Tests
{
    public abstract class TestsBase
    {
        protected IServiceProvider ConfigureProvider(Action<IServiceCollection> configure)
        {
            var services = new ServiceCollection();

            configure(services);

            return services.BuildServiceProvider();
        }
    }
}