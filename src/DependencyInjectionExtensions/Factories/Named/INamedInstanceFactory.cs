namespace DependencyInjectionExtensions.Factories.Named
{
    public interface INamedInstanceFactory<out TInterface>
        where TInterface : class
    {
        TInterface Resolve(string name);
        
        TInterface Resolve(string name, object[] arguments);
    }
    
    public interface INamedInstanceFactory
    {
        object Resolve(string name);
        
        object Resolve(string name, object[] arguments);
    }


   
}