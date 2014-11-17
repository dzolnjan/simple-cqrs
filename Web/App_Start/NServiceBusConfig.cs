using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Input;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Cqrs.Core.EventHandler;
using Cqrs.Core.Infrastructure;
using NServiceBus;
using NServiceBus.Persistence;
using Raven.Client.Document;

namespace Cqrs.Web
{
    public class NServiceBusConfig
    {
        public static void Configure(IContainer container)
        {
            var configuration = new BusConfiguration();

            //configuration.LicensePath(HttpRuntime.BinDirectory + @"License\License.xml");

            var documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "Cqrs.Web",
                ResourceManagerId = Guid.NewGuid()
            };

            documentStore.Initialize();
            configuration.UsePersistence<RavenDBPersistence>().SetDefaultDocumentStore(documentStore);

            configuration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(container));
            configuration.UseSerialization<XmlSerializer>();
            configuration.UseTransport<MsmqTransport>();
            configuration.Transactions();
            configuration.EnableInstallers();
            //configuration.LoadMessageHandlers(First<NewBlogAddedEventHandler>.Then<NewBlogAddedEventHandler>());

            var busStarter = Bus.Create(configuration);

            var bus = busStarter.Start();   
        }

    }
}