using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionExtensions.Tests.Core
{
    public abstract class TestsBase
    {
        protected IServiceCollection Services { get; private set; }
        
        protected IServiceProvider ConfigureProvider(Action<IServiceCollection> configure)
        {
            Services = new ServiceCollection();

            configure(Services);

            return Services.BuildServiceProvider();
        }
    }
}