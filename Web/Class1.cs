using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cqrs.Core.Command;
using Cqrs.Core.Event;
using Cqrs.Core.Infrastructure;
using NServiceBus;

namespace Cqrs.Web
{
    //public class NewBlogAddedEventHandler1 : IHandleMessages<NewBlogAddedEvent>
    //{
    //    private readonly ICommander _commander;

    //    public NewBlogAddedEventHandler1(ICommander commander)
    //    {
    //        _commander = commander;
    //    }

    //    public void Handle(NewBlogAddedEvent message)
    //    {
    //        _commander.Execute(new AddWelcomePost()
    //        {
    //            Id = message.Id,
    //            PostTitle = "This is the welcome post created from event handler 11111111"
    //        });
    //    }
    //}
}