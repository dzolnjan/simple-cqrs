using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Cqrs.Core.Infrastructure;


namespace Cqrs.Console
{
    public static class AutofacConfig
    {
        public static IContainer RegisterDependencies()
        {  
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(Commander).Assembly)
                .AsImplementedInterfaces()
                .SingleInstance();   

            var container = builder.Build();

            var builder2 = new ContainerBuilder();
            builder2.RegisterInstance(container);
            builder2.Update(container);

            return container;
        }
    }
}
