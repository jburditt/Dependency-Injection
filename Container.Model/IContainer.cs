using System;

namespace Container.Model
{
    public interface IContainer<TContainer>
    {
        T ResolveInstance<T>() 
            where T : class;
        void RegisterInstance(Type type, object instance, Lifetime lifetime = Lifetime.Default);
        void RegisterInstance<TService, TImplementation>(object instance, Lifetime lifetime = Lifetime.Default)
            where TService : class
            where TImplementation : class, TService;
        void RegisterInstance<TService, TImplementation>(Lifetime lifetime = Lifetime.Default)
            where TService : class
            where TImplementation : class, TService;
        void RegisterDependencies(ContainerSettings settings);
        IContainer<TContainer> PostRegister();
        IContainer<TContainer> VerifyContainer();
    }
}