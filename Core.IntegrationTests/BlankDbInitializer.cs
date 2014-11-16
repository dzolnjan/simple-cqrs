using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Core.Persistance;

namespace Cqrs.Core.IntegrationTests
{
    public class MyDatabaseInitializer : System.Data.Entity.DropCreateDatabaseAlways<BloggingContext>
    {
        protected override void Seed(BloggingContext context)
        {
            // Add entities to database.

            context.SaveChanges();
        }
    }
}
