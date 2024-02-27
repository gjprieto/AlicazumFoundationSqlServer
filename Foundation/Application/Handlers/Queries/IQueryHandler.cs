using Foundation.Application.Messages;
using Foundation.Application.Messages.Commands;
using Foundation.Application.Messages.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Handlers.Commands
{
    public interface IQueryHandler<in TMessage, TResult> : IMessageHandler<TMessage, TResult>
        where TMessage : IQuery
    {
    }
}