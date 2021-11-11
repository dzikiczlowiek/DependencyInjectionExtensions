using System;

namespace DependencyInjectionExtensions.Factories.Named
{
    internal class NamedInstanceSelector<TInterface> : INamedInstanceSelector<TInterface>
        where TInterface : class
    {
        private readonly string _name;

        public NamedInstanceSelector(string name, Type type)
        {
            _name = name;
            Type = type;
        }

        public Type Type { get; }

        public bool IsApplicable(string name) => _name == name;
    }
}