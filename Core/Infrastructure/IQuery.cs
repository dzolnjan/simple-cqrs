using System;
using System.Collections.Generic;
using Autofac;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Core.Infrastructure
{    
    public interface IQuery<TResult> { }

    public interface IHandleQuery<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }

    public interface IQuerier
    {
        TResult Execute<TResult>(IQuery<TResult> query);
    }

    public class Querier : IQuerier
    {
        private readonly IContainer _container;

        public Querier(IContainer container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IHandleQuery<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _container.Resolve(handlerType);
            return handler.Handle((dynamic)query);
        }
    }
}
