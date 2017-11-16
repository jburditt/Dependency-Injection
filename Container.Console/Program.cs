using System;
using Container.SimpleInjector;
using Container.Model;

namespace Container.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new SimpleInjectorContainer(null)
                .Register(x=> 
                {
                    x.RegisterInstance<IUser, User>();
                })
                .Verify();

            var user = container.ResolveInstance<IUser>();
        }
    }
}