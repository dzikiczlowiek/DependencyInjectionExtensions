using System;

namespace DependencyInjectionExtensions.Factories.Named
{
    public interface INamedInstanceSelector<in TInterface>
        where TInterface : class
    {
        public Type Type { get; }

        public bool IsApplicable(string name);
    }
}