using Foundation.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Handlers
{
    public interface IMessageHandler<in TMessage> where TMessage: IMessage
    {
        Task<IMessageHandlerResult> HandleAsync(TMessage message);
    }

    public interface IMessageHandler<in TMessage, TResult> where TMessage : IMessage
    {
        Task<IMessageHandlerResult<TResult>> HandleAsync(TMessage message);
    }
}