using System;

namespace DependencyInjectionExtensions.Tests.TestsComponents
{
    public interface IHaveInstanceId
    {
        public Guid Id { get; }
    }
}