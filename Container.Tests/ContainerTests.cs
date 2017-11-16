using Microsoft.VisualStudio.TestTools.UnitTesting;
using Container.SimpleInjector;
using Container.Model;

namespace Container.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void Resolve_Instance()
        {
            var container = new SimpleInjectorContainer(null, x => {
                x.RegisterInstance<IUser, User>();
            })
            .Verify();

            var user = container.ResolveInstance<IUser>();

            Assert.IsNotNull(user);
        }
    }
}