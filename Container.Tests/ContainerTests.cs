﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Container.SimpleInjector;

namespace Container.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void Resolve_Instance()
        {
            var container = ContainerConfig.Register<CustomSimpleInjectorContainer>()
                    .PostRegister()
                    .Verify();

            var user = container.Current.ResolveInstance<IUser>();

            Assert.IsNotNull(user);
        }
    }
}