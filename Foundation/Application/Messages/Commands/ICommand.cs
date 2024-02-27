using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Messages.Commands
{
    public interface ICommand : IMessage
    {
    }

    public interface ICommand<TInput> : IMessage<TInput>, ICommand
    {
    }

    public interface ICommand<TInput, TValidator> : IMessage<TInput, TValidator>, ICommand<TInput>
        where TValidator : IValidator<TInput>
    {
    }
}