using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cqrs.Core.Command;
using Cqrs.Core.Persistance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cqrs.Core.IntegrationTests
{
    [TestClass]

    public class IntegrationTestBase
    {  
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            System.Data.Entity.Database.SetInitializer(new MyDatabaseInitializer());
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var context = new BloggingContext();
            context.Database.Initialize(true);
        }
    }
}
