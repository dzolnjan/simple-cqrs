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
    public class AddWelcomePost : ICommand<Task<AddWelcomePostResults>>
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
    }

    public class AddWelcomePostResults
    {
        public Post Post { get; set; }
    }

    public class AddWelcomePostHandler : IHandleCommand<AddWelcomePost, Task<AddWelcomePostResults>>
    {
        private readonly IBus _bus;

        public AddWelcomePostHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task<AddWelcomePostResults> Handle(AddWelcomePost command, BloggingContext context = null)
        {
            // validations
            if (command.PostTitle.IsNullOrBlank())
            {
                throw new ArgumentNullException(command.PostTitle);
            }

            if (command.PostTitle.Length > 100)
            {
                throw new BusinessRuleException("Blog name is longer than 30 chars.");
            }

            // allow for passing in context and it is only for testing purposes
            context = context ?? new BloggingContext();

            var blog = await context.Blogs.SingleOrDefaultAsync(x => x.BlogId == command.Id);

            if (blog == null)
            {
                throw new ObjectNotFoundException("Blog with id {0} is not found.".Fmt(command.Id));
            }

            var post = new Post()
                       {
                           Title = command.PostTitle
                       };

            blog.Posts.Add(post);

            await context.SaveChangesAsync();

            return new AddWelcomePostResults()
                   {
                       Post = post
                   };
        }
    }
}
