using System;

namespace DependencyInjectionExtensions.Tests.TestsComponents
{
    public class KungFoo : IFoo
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}