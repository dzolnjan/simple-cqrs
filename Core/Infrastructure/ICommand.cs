using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Cqrs.Core.Persistance;

namespace Cqrs.Core.Infrastructure
{
    public interface ICommand<TResult> { }

    public interface IHandleCommand<in TQuery, out TResult> where TQuery : ICommand<TResult>
    {
        TResult Handle(TQuery query, BloggingContext context = null);
    } 

    public interface ICommander
    {
        TResult Execute<TResult>(ICommand<TResult> command);
    }

    public class Commander : ICommander
    {
        private readonly IContainer _container;

        public Commander(IContainer container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public TResult Execute<TResult>(ICommand<TResult> command)
        {
            var handlerType = typeof(IHandleCommand<,>).MakeGenericType(command.GetType(), typeof(TResult));
            dynamic handler = _container.Resolve(handlerType);
            return handler.Handle((dynamic)command);
        }
    }
}
