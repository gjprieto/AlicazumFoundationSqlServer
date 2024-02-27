using Foundation.Application.Messages;
using Foundation.Application.Messages.Commands;
using Foundation.Application.Messages.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Handlers.Commands
{
    public abstract class QueryHandlerBase<TMessage, TResult> : MessageHandlerBase<TMessage, TResult>, IQueryHandler<TMessage, TResult>
        where TMessage : IQuery
    {
        protected QueryHandlerBase(ILogger logger) : base(logger)
        {
        }
    }
}