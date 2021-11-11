using System;

namespace DependencyInjectionExtensions.Tests.TestsComponents
{
    public class BooFoo : IFoo
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}