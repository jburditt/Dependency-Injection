using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inject.SimpleInjector;
using Inject.Model;

namespace Inject.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void Resolve_Instance()
        {
            var container = new Injector(null)
                .Register(x => {
                    x.RegisterInstance<IUser, User>();
                })
                .Verify();

            var user = container.ResolveInstance<IUser>();

            Assert.IsNotNull(user);
        }
    }
}