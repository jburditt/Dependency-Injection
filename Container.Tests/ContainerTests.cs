using System;
//using Container;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Container.SimpleInjector;

namespace Container.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            ContainerConfig.Register<CustomSimpleInjectorContainer>()
                    .PostRegister()
                    .Verify();
        }
    }
}