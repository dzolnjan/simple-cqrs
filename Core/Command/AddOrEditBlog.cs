using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Core.Domain;
using Cqrs.Core.Event;
using Cqrs.Core.Infrastructure;
using Cqrs.Core.Persistance;
using Cqrs.Core.Query;
using Cqrs.Core.Utils;
using NServiceBus;

namespace Cqrs.Core.Command
{
    public class AddOrEditBlog : ICommand<Task<AddOrEditBlogResults>>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class AddOrEditBlogResults 
    {
        public Blog Blog { get; set; }
    }

    public class AddOrEditBlogHandler : IHandleCommand<AddOrEditBlog, Task<AddOrEditBlogResults>>
    {
        private readonly IBus _bus;

        public AddOrEditBlogHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task<AddOrEditBlogResults> Handle(AddOrEditBlog command, BloggingContext context = null)
        {
            // validations
            if (command.Name.IsNullOrBlank())
            {
                throw new ArgumentNullException(command.Name);
            }

            if (command.Name.Length > 30)
            {
                throw new BusinessRuleException("Blog name is longer than 30 chars.");
            }

            // allow for passing in context and it is only for testing purposes
            context = context ?? new BloggingContext();

            Blog blog = null;

            if (command.Id.HasValue)
            {
                blog = await context.Blogs.SingleOrDefaultAsync(x => x.BlogId == command.Id);

                if (blog == null)
                {
                    throw new ObjectNotFoundException("Blog with id {0} is not found.".Fmt(command.Id)); 
                }
            }
            else
            {
                blog = new Blog();
                context.Blogs.Add(blog);
            }

            blog.Name = command.Name; 

            await context.SaveChangesAsync();

            if (_bus != null)
            {
                _bus.Publish(new NewBlogAddedEvent() { Id = blog.BlogId });
            }

            return new AddOrEditBlogResults()
                   {
                       Blog = blog
                   };
        }
    }
}
