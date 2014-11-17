using System;
using System.IO;
using Cqrs.Core.Command;
using Cqrs.Core.Domain;
using Cqrs.Core.Persistance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NServiceBus;
using Should;

namespace Cqrs.Core.IntegrationTests.Command
{
    [TestClass]
    public class AddOrEditBlogTests : IntegrationTestBase
    {
        private readonly AddOrEditBlogHandler _addOrEditBlogHandler = new AddOrEditBlogHandler(null);

        [TestMethod]
        public void Can_Add_New_Blog()
        { 
            // arrange
            var command = new AddOrEditBlog() { Name = "blog123" }; 

            // act
            var result = _addOrEditBlogHandler.Handle(command);

            // assert
            result.Result.Blog.ShouldNotBeNull();
            result.Result.Blog.Name.ShouldEqual(command.Name);
        }
    }
}
