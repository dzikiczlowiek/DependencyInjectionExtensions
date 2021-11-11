using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionExtensions.Factories.Typed
{
    internal class TypedInstanceFactory<TInterface> : ITypedInstanceFactory<TInterface>, ITypedInstanceFactory
        where TInterface : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<ITypedInstanceSelector<TInterface>> _selectors;

        public TypedInstanceFactory(IServiceProvider serviceProvider,
            IEnumerable<ITypedInstanceSelector<TInterface>> selectors)
        {
            _serviceProvider = serviceProvider;
            _selectors = selectors;
        }

        object ITypedInstanceFactory.Resolve(Type type)
        {
            return Resolve(type);
        }

        object ITypedInstanceFactory.Resolve(Type type, object parameter)
        {
            return Resolve(type, parameter);
        }

        public TInterface Resolve(Type type, object parameter)
        {
            try
            {
                var selector = _selectors.Single(s => s.IsApplicable(type));
                return (TInterface) ActivatorUtilities.CreateInstance(_serviceProvider, selector.Type,
                    selector.GetArgs(parameter));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TInterface Resolve(Type type)
        {
            throw new NotImplementedException();
        }
    }
}