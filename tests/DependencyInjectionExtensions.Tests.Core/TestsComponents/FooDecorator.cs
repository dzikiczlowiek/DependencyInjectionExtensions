using System;

namespace DependencyInjectionExtensions.Tests.TestsComponents
{
    public class FooDecorator : IFoo
    {
        private readonly IFoo _inner;

        public FooDecorator(IFoo inner)
        {
            _inner = inner;
        }

        public Guid Id { get; } = Guid.NewGuid();

        public IFoo Inner => _inner;
    }
}