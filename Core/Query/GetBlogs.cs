using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Core.Domain;
using Cqrs.Core.Infrastructure;
using Cqrs.Core.Persistance;

namespace Cqrs.Core.Query
{
    public class GetBlogsByName : IQuery<Task<GetBlogsByNameResults>>
    {
        public string Name { get; set; }
    }

    public class GetBlogsByNameResults
    {
        public IList<Blog> Blogs { get; set; }  
    }

    public class GetBlogsByNameHandler : IHandleQuery<GetBlogsByName, Task<GetBlogsByNameResults>>
    {
        public async Task<GetBlogsByNameResults> Handle(GetBlogsByName query)
        {
            var context = new BloggingContext();

            var list = await context.Blogs.Where(x => x.Name.ToLower().Contains(query.Name)).ToListAsync();

            return new GetBlogsByNameResults()
                   {
                       Blogs = list
                   };
        }
    }
}
