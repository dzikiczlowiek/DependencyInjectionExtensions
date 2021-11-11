using System;

namespace DependencyInjectionExtensions.Factories.Typed
{
    public interface ITypedInstanceFactory<out TInterface>
        where TInterface : class
    {
        TInterface Resolve(Type type);

        TInterface Resolve(Type type, object parameter);
    }
    
    public interface ITypedInstanceFactory
    {
        object Resolve(Type type);

        object Resolve(Type type, object parameter);
    }
}