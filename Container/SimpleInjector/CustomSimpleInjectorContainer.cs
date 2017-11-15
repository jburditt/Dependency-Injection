using System.Web;
using SimpleInjector;
using System.Collections.Generic;
using System;
using System.Reflection;
using SimpleInjector.Integration.Web;
using SimpleInjector.Diagnostics;
using Container.Model;
using SimpleInjectorContainer = SimpleInjector.Container;

namespace Container.SimpleInjector
{
    /// <summary>
    /// wrapper for simple injector resolution.
    /// </summary>
    public class CustomSimpleInjectorContainer : IContainer<CustomSimpleInjectorContainer>, IResolver
    {
        private SimpleInjectorContainer container = new SimpleInjectorContainer();

        public T ResolveInstance<T>() where T : class
        {
            return container.GetInstance<T>();
        }

        public void RegisterInstance(Type type, object instance, Lifetime lifetime = Lifetime.Default)
        {
            if (lifetime == Lifetime.Default)
                container.Register(type, () => instance);
            else
                container.Register(type, () => instance, lifetime.ToLifestyle());
        }

        public void RegisterInstance<TService, TImplementation>(object instance, Lifetime lifetime = Lifetime.Default)
            where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService>(() => (TImplementation)instance);
        }

        public void RegisterInstance<TService, TImplementation>(Lifetime lifetime = Lifetime.Default) 
            where TService : class
            where TImplementation : class, TService
        {
            if (lifetime == Lifetime.Default)
                container.Register<TService, TImplementation>();
            else
                container.Register<TService, TImplementation>(lifetime.ToLifestyle());
        }

        public void RegisterDependencies(ContainerSettings settings)
        {
            // set default scope lifestyle to be per thread
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            if (settings != null)
            {
                // allow overriding registrations
                if (settings.AllowOverridingRegistrations)
                    container.Options.AllowOverridingRegistrations = true;
            }

            //BaseContainer.RegisterDependencies(this);
            this.RegisterInstance<IUser, User>();

            //this.Register<IAuthenticationManager>(() => IsVerifying
            //        ? new OwinContext(new Dictionary<string, object>()).Authentication
            //        : HttpContext.Current.GetOwinContext().Authentication);

            //container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
        }

        public IContainer<CustomSimpleInjectorContainer> PostRegister()
        {
            // [Lifestyle Mismatch] NLogLogging (Singleton) depends on ILoggingConnection (Transient).
            //this.GetRegistration(typeof(ILogging)).Registration
            //    .SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Unit of Work is always disposed, ignore warning");

            return this;
        }

        public IContainer<CustomSimpleInjectorContainer> VerifyContainer() {
            container.Verify();

            return this;
        }
    }
}