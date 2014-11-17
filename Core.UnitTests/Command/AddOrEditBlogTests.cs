using System;
using Cqrs.Core.Command;
using Cqrs.Core.Domain;
using Cqrs.Core.Event;
using Cqrs.Core.Persistance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using MSTestExtensions;
using NServiceBus;
using Should;


namespace Cqrs.Core.UnitTests.Command
{
    [TestClass]
    public class AddOrEditBlogTests
    {
        public static readonly IAssertion Assert = new Assertion();

        [TestMethod]
        public void Can_Add_New_Blog()
        {
            var command = new AddOrEditBlog() { Name = "blog123" };
            var mockSet = new Mock<DbSet<Blog>>();
            var mockContext = new Mock<BloggingContext>();
            mockContext.Setup(m => m.Blogs).Returns(mockSet.Object);

            //var mockBus = new Mock<IBus>();
            //mockBus.Setup(bus => bus.Publish<NewBlogAddedEvent>(It.IsAny<IMessage>()))
            //      .Verifiable();

            var handler = new AddOrEditBlogHandler(null);
            var result = handler.Handle(command, mockContext.Object);

            result.Result.Blog.ShouldNotBeNull();
            result.Result.Blog.Name.ShouldEqual(command.Name);
        }

        [TestMethod]
        public void Cannot_Add_New_Blog_When_Name_Is_NullOrEmpty()
        {
            var command = new AddOrEditBlog() { Name = "" };
            var mockSet = new Mock<DbSet<Blog>>();
            var mockContext = new Mock<BloggingContext>();
            mockContext.Setup(m => m.Blogs).Returns(mockSet.Object);

            var handler = new AddOrEditBlogHandler(null);

            Assert.Throws<ArgumentNullException>(() => handler.Handle(command, mockContext.Object)); 
        }
    }
}
