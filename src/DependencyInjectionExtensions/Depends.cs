using System;

namespace DependencyInjectionExtensions
{
    public sealed record Depends
    {
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public string? Named { get; }

        public Depends(Type ServiceType, Type ImplementationType, string? Named = null)
        {
            this.ServiceType = ServiceType;
            this.ImplementationType = ImplementationType;
            this.Named = Named;
        }
      
    }

    public static class DependsExtensions
    {
        public static Depends DependsOn(this Type serviceType, Type implementationType, string? named = null)
        {
            return new Depends(serviceType, implementationType, named);
        }
    }
}