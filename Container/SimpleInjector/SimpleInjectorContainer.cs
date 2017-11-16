using System;
using Inject.Model;
using SimpleInjector;

namespace Inject.SimpleInjector
{
    /// <summary>
    /// wrapper for simple injector resolution.
    /// </summary>
    public class Injector : IContainer
    {
        public Container Container;

        public Injector(ContainerSettings settings = null)
        {
            Container = new Container();

            if (settings != null)
            {
                // allow overriding registrations
                if (settings.AllowOverridingRegistrations)
                    Container.Options.AllowOverridingRegistrations = true;
            }
        }

        public IContainer Register(Action<Injector> registerDependencies)
        {
            registerDependencies.Invoke(this);

            return this;
        }

        public T ResolveInstance<T>() where T : class
        {
            return Container.GetInstance<T>();
        }

        public void RegisterInstance(Type type, object instance, Lifetime lifetime = Lifetime.Default)
        {
            if (lifetime == Lifetime.Default)
                Container.Register(type, () => instance);
            else
                Container.Register(type, () => instance, lifetime.ToLifestyle());
        }

        public void RegisterInstance<TService, TImplementation>(object instance, Lifetime lifetime = Lifetime.Default)
            where TService : class
            where TImplementation : class, TService
        {
            Container.Register<TService>(() => (TImplementation)instance);
        }

        public void RegisterInstance<TService, TImplementation>(Lifetime lifetime = Lifetime.Default) 
            where TService : class
            where TImplementation : class, TService
        {
            if (lifetime == Lifetime.Default)
                Container.Register<TService, TImplementation>();
            else
                Container.Register<TService, TImplementation>(lifetime.ToLifestyle());
        }

        public IContainer Verify()
        {
            Container.Verify();

            return this;
        }
    }
}