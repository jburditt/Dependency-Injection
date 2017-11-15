using System;
using Container.SimpleInjector;

namespace Container.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new CustomSimpleInjectorContainer(null, x => {
                x.RegisterInstance<IUser, User>();
            })
            .Verify();

            var user = container.ResolveInstance<IUser>();
        }
    }
}