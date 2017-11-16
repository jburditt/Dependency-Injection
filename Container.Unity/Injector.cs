using System;
using Unity;
using Inject.Model;
using Unity.Lifetime;

namespace Container.Unity
{
    /// <summary>
    /// wrapper for unity resolution.
    /// </summary>
    public class Injector : UnityContainer, IContainer
    {
        public T ResolveInstance<T>() where T : class
        {
            return this.Resolve<T>();
        }

        public void RegisterInstance(Type type, object instance, Lifetime lifetime = Lifetime.Default)
        {
            UnityContainerExtensions.RegisterInstance(this, type, instance);
        }

        public void RegisterInstance<TService, TImplementation>(object instance, Lifetime lifetime = Lifetime.Default)
            where TService : class
            where TImplementation : class, TService
        {
            UnityContainerExtensions.RegisterInstance(this, typeof(TService), instance);
        }

        public void RegisterInstance<TService, TImplementation>(Lifetime lifetime = Lifetime.Default)
            where TService : class
            where TImplementation : class, TService
        {
            switch (lifetime)
            {
                case Lifetime.Scoped:
                    this.RegisterType<TService, TImplementation>(new PerThreadLifetimeManager());
                    break;
                case Lifetime.Singleton:
                    this.RegisterType<TService, TImplementation>(new ContainerControlledLifetimeManager());
                    break;
                case Lifetime.Transient:
                    this.RegisterType<TService, TImplementation>(new TransientLifetimeManager());
                    break;
                case Lifetime.Default:
                default:
                    this.RegisterType<TService, TImplementation>();
                    break;
            }
        }

        public IContainer Verify() {
            return this;
        }
    }
}