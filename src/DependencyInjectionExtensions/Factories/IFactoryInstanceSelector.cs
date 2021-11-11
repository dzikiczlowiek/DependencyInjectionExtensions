using System;

namespace DependencyInjectionExtensions.Factories
{
    public interface IFactoryInstanceSelector<in TInterface, TImplementation>
        where TInterface : class
        where TImplementation : class, TInterface
    {
        public Type Type => typeof(TImplementation);

        public bool IsApplicable(TInterface instance);
    }
}