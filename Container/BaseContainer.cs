using Container.Model;

namespace Container
{
    public class BaseContainer
    {
        public static void RegisterDependencies<T>(T container) where T : IContainer<T>
        {
            container.RegisterInstance<IEmailService, ExactTargetEmailService>();
        }
    }
}