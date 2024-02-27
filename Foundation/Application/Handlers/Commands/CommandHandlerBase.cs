using Foundation.Application.Messages;
using Foundation.Application.Messages.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Handlers.Commands
{
    public abstract class CommandHandlerBase<TMessage> : MessageHandlerBase<TMessage>, ICommandHandler<TMessage>
        where TMessage : ICommand
    {
        protected CommandHandlerBase(ILogger logger) : base(logger)
        {
        }
    }

    public abstract class CommandHandlerBase<TMessage, TResult> : MessageHandlerBase<TMessage, TResult>, ICommandHandler<TMessage, TResult>
        where TMessage : ICommand
    {   
        protected CommandHandlerBase(ILogger logger) : base(logger)
        {
        }
    }
}