using System;
using Container.Model;
using BaseContainer = SimpleInjector.Container;

namespace Container.SimpleInjector
{
    /// <summary>
    /// wrapper for simple injector resolution.
    /// </summary>
    public class SimpleInjectorContainer : IContainer
    {
        public BaseContainer BaseContainer;

        public SimpleInjectorContainer(ContainerSettings settings = null)
        {
            BaseContainer = new BaseContainer();

            if (settings != null)
            {
                // allow overriding registrations
                if (settings.AllowOverridingRegistrations)
                    BaseContainer.Options.AllowOverridingRegistrations = true;
            }
        }

        public IContainer Register(Action<SimpleInjectorContainer> registerDependencies)
        {
            registerDependencies.Invoke(this);

            return this;
        }

        public T ResolveInstance<T>() where T : class
        {
            return BaseContainer.GetInstance<T>();
        }

        public void RegisterInstance(Type type, object instance, Lifetime lifetime = Lifetime.Default)
        {
            if (lifetime == Lifetime.Default)
                BaseContainer.Register(type, () => instance);
            else
                BaseContainer.Register(type, () => instance, lifetime.ToLifestyle());
        }

        public void RegisterInstance<TService, TImplementation>(object instance, Lifetime lifetime = Lifetime.Default)
            where TService : class
            where TImplementation : class, TService
        {
            BaseContainer.Register<TService>(() => (TImplementation)instance);
        }

        public void RegisterInstance<TService, TImplementation>(Lifetime lifetime = Lifetime.Default) 
            where TService : class
            where TImplementation : class, TService
        {
            if (lifetime == Lifetime.Default)
                BaseContainer.Register<TService, TImplementation>();
            else
                BaseContainer.Register<TService, TImplementation>(lifetime.ToLifestyle());
        }

        public IContainer Verify()
        {
            BaseContainer.Verify();

            return this;
        }
    }
}