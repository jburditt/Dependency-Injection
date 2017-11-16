using Inject.Model;
using Inject.SimpleInjector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace Inject.Web
{
    public class MvcInjector : SimpleInjector.Injector
    {
        public MvcInjector(ContainerSettings settings, IServiceCollection services)
        {
            var injector = new SimpleInjector.Injector(settings);

            injector.Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(injector.Container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(injector.Container));

            services.EnableSimpleInjectorCrossWiring(injector.Container);
            services.UseSimpleInjectorAspNetRequestScoping(injector.Container);
        }

        public void RegisterMvc(IApplicationBuilder app)
        {
            Container.RegisterMvcControllers(app);
            Container.RegisterMvcViewComponents(app);
        }
    }
}