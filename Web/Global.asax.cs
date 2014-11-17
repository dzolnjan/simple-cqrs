using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Cqrs.Core.Event;
using NServiceBus;

namespace Cqrs.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            var container = AutofacConfig.RegisterDependencies();
            NServiceBusConfig.Configure(container);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var eee = container.Resolve<IHandleMessages<NewBlogAddedEvent>>();
            eee.Handle(new NewBlogAddedEvent() { Id = 10 });


        }
    }
}
