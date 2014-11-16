using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Cqrs.Core.Command;
using Cqrs.Core.Infrastructure;

namespace Cqrs.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = AutofacConfig.RegisterDependencies();

            var commander = container.Resolve<ICommander>();

            Task.Run(async () =>
            {
                var commandResult = await commander.Execute(new AddOrEditBlog() { Name = "console blog" });

            }).Wait();  
        }
    }
}
