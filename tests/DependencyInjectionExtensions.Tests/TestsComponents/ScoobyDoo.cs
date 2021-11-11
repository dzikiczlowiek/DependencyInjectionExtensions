using System;

namespace DependencyInjectionExtensions.Tests.TestsComponents
{
    public class ScoobyDoo : IScooby
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}