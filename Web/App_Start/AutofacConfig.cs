using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Input;
using Autofac;
using Autofac.Integration.Mvc;
using Cqrs.Core.Infrastructure;

namespace Cqrs.Web
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
      
            var builder = new ContainerBuilder();

            // placed here before RegisterControllers as last one wins
            builder.RegisterAssemblyTypes(typeof (Commander).Assembly)
                .AsImplementedInterfaces()
                .SingleInstance();  

            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                .InstancePerRequest();

            var container = builder.Build();

            var builder2 = new ContainerBuilder();
            builder2.RegisterInstance(container);
            builder2.Update(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));  
        }

    }
}