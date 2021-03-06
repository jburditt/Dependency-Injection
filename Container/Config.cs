﻿//using System.Web.Http;
using System.Web.Mvc;
using SimpleInjector.Integration.Web.Mvc;
//using SimpleInjector.Integration.WebApi;
using SimpleInjectorContainer = SimpleInjector.Container;
//using MvcUnity = Unity.Mvc5;
//using Unity.WebApi;
//using Microsoft.Practices.Unity;
using Container.Model;
using Container.SimpleInjector;

namespace Container
{
    public static class Config
    {
        public static IContainer Initialize<T>(IContainer container, ContainerSettings settings = null)
        {
            if (container is CustomSimpleInjectorContainer)
            {
                DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container as SimpleInjectorContainer));
                GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(Container.Current as SimpleInjectorContainer);

                Global.Container = container as CustomSimpleInjectorContainer;
            }

            //if (Container.Current is CustomUnityContainer)
            //{
            //    DependencyResolver.SetResolver(new MvcUnity.UnityDependencyResolver(Container.Current as IUnityContainer));
            //    GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(Container.Current as IUnityContainer);

            //    Global.Container = Container.Current as CustomUnityContainer;
            //}

            return Container;
        }
    }
}