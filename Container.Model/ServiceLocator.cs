using System.Web.Mvc;

namespace Container.Model
{
    public static class ServiceLocator
    {
        public static T Resolve<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
    }
}