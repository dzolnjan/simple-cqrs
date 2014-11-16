using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Core.Domain;

namespace Cqrs.Core.Persistance
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("CqrsDatabase") {}
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
    }
}
