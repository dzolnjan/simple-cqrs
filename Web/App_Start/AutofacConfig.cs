using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Input;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cqrs.Core.Event;
using Cqrs.Core.EventHandler;
using Cqrs.Core.Infrastructure;
using NServiceBus;

namespace Cqrs.Web
{
    public class AutofacConfig
    {
        public static IContainer RegisterDependencies()
        {  
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof (Commander).Assembly)
                .Where(t => !t.Name.EndsWith("EventHandler"))   // let nservicebus register its handlers from Core
                .AsImplementedInterfaces()
                .SingleInstance();  

            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                .InstancePerRequest();

            var container = builder.Build();

            var builder2 = new ContainerBuilder();
            builder2.RegisterInstance(container);
            builder2.Update(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


            return container;
        }

    }
}