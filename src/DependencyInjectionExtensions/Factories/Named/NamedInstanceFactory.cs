using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionExtensions.Factories.Named
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class NamedInstanceFactory<TInterface> : INamedInstanceFactory<TInterface>, INamedInstanceFactory
        where TInterface : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<INamedInstanceSelector<TInterface>> _selectors;

        public NamedInstanceFactory(IServiceProvider serviceProvider, IEnumerable<INamedInstanceSelector<TInterface>> selectors)
        {
            _serviceProvider = serviceProvider;
            _selectors = selectors;
        }
        
        public TInterface Resolve(string name)
        {
            try
            {
                var instance = _selectors.Single(s => s.IsApplicable(name));
                return (TInterface) _serviceProvider.GetRequiredService(instance.Type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TInterface Resolve(string name, object[] arguments)
        {
            try
            {
                var instance = _selectors.Single(s => s.IsApplicable(name));
                return (TInterface) ActivatorUtilities.CreateInstance(_serviceProvider, instance.Type, arguments);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        object INamedInstanceFactory.Resolve(string name, object[] arguments)
        {
            return Resolve(name, arguments);
        }

        object INamedInstanceFactory.Resolve(string name)
        {
            return Resolve(name);
        }
    }
}