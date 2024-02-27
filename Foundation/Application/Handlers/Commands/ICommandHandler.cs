using Foundation.Application.Messages;
using Foundation.Application.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Handlers.Commands
{
    public interface ICommandHandler<in TMessage> : IMessageHandler<TMessage>
        where TMessage : ICommand
    {
    }

    public interface ICommandHandler<in TMessage, TResult> : IMessageHandler<TMessage, TResult>
        where TMessage : ICommand
    {
    }
}
