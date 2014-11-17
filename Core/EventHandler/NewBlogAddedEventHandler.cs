using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Core.Command;
using Cqrs.Core.Event;
using Cqrs.Core.Infrastructure;
using NServiceBus;

namespace Cqrs.Core.EventHandler
{
    public class NewBlogAddedEventHandler : IHandleMessages<NewBlogAddedEvent>
    {
        private readonly ICommander _commander;

        public NewBlogAddedEventHandler(ICommander commander)
        {
            _commander = commander;
        }

        public void Handle(NewBlogAddedEvent message)
        {
            _commander.Execute(new AddWelcomePost()
                {
                    Id = message.Id,
                    PostTitle = "This is the welcome post created from event handler"
                }); 
        }
    }
}
