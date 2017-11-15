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
    public class CustomSimpleInjectorContainer : SimpleInjectorContainer, IContainer<CustomSimpleInjectorContainer>, IResolver
    {
        public T ResolveInstance<T>() where T : class
        {
            return this.GetInstance<T>();
        }

        public void RegisterInstance(Type type, object instance, Lifetime lifetime = Lifetime.Default)
        {
            if (lifetime == Lifetime.Default)
                this.Register(type, () => instance);
            else
                this.Register(type, () => instance, lifetime.ToLifestyle());
        }

        public void RegisterInstance<TService, TImplementation>(object instance, Lifetime lifetime = Lifetime.Default)
            where TService : class
            where TImplementation : class, TService
        {
            this.Register<TService>(() => (TImplementation)instance);
        }

        public void RegisterInstance<TService, TImplementation>(Lifetime lifetime = Lifetime.Default) 
            where TService : class
            where TImplementation : class, TService
        {
            if (lifetime == Lifetime.Default)
                this.Register<TService, TImplementation>();
            else
                this.Register<TService, TImplementation>(lifetime.ToLifestyle());
        }

        public void RegisterDependencies(ContainerSettings settings)
        {
            // set default scope lifestyle to be per thread
            this.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            if (settings != null)
            {
                // allow overriding registrations
                if (settings.AllowOverridingRegistrations)
                    this.Options.AllowOverridingRegistrations = true;
            }

            //BaseContainer.RegisterDependencies(this);

            //this.Register<IAuthenticationManager>(() => IsVerifying
            //        ? new OwinContext(new Dictionary<string, object>()).Authentication
            //        : HttpContext.Current.GetOwinContext().Authentication);

            this.RegisterMvcControllers(Assembly.GetExecutingAssembly());
        }

        public IContainer<CustomSimpleInjectorContainer> PostRegister()
        {
            // [Lifestyle Mismatch] NLogLogging (Singleton) depends on ILoggingConnection (Transient).
            //this.GetRegistration(typeof(ILogging)).Registration
            //    .SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Unit of Work is always disposed, ignore warning");

            return this;
        }

        public IContainer<CustomSimpleInjectorContainer> VerifyContainer() {
            this.Verify();

            return this;
        }
    }
}