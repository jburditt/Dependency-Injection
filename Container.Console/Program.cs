using Inject.SimpleInjector;
using Inject.Model;

namespace Inject.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Injector(null)
                .Register(x => 
                {
                    x.RegisterInstance<IUser, User>();
                })
                .Verify();

            var user = container.ResolveInstance<IUser>();
        }
    }
}