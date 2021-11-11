using System;

namespace DependencyInjectionExtensions.Tests.TestsComponents
{
    public class BarService : IBar
    {
        public BarService(IFoo foo, IScooby scooby)
        {
            Foo = foo;
            Scooby = scooby;
        }

        public IFoo Foo { get; }
        public IScooby Scooby { get; }

        public Guid Id { get; } = Guid.NewGuid();

    }
}