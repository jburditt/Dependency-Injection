using System;
using Container.SimpleInjector;

namespace Container.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Register<CustomSimpleInjectorContainer>()
                    .PostRegister()
                    .Verify();

            var user = container.Current.ResolveInstance<IUser>();
        }
    }
}
