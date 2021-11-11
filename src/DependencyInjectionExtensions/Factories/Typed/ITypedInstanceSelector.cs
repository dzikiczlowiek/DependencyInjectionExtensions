using System;

namespace DependencyInjectionExtensions.Factories.Typed
{
    public interface ITypedInstanceSelector<in TInterface>
        where TInterface : class
    {
        public Type Type { get; }

        public bool IsApplicable(Type type);

        object[] GetArgs(object instance);
    }
}