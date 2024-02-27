using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Handlers
{
    public interface IMessageHandlerResult
    {
        bool IsValid { get; }
        bool IsCompleted { get; }
        string[] Warnings { get; }
        string[] Errors { get; }
    }

    public interface IMessageHandlerResult<out TResult> : IMessageHandlerResult
    {
        TResult? Result { get; }
    }
}