using System;

namespace DependencyInjectionExtensions.Factories.Typed
{
    internal class TypedInstanceSelector<TInterface> : ITypedInstanceSelector<TInterface>
        where TInterface : class
    {
        private readonly Type _triggerType;
        private readonly Func<object, object[]> _getArguments;

        public TypedInstanceSelector(Type type, Type triggerType, Func<object, object[]> getArguments)
        {
            _triggerType = triggerType;
            _getArguments = getArguments;
            Type = type;
        }
        
        public Type Type { get; }

        public object[] GetArgs(object instance) => _getArguments(instance);
        
        public bool IsApplicable(Type type)
        {
            return _triggerType == type;
        }
    }
}