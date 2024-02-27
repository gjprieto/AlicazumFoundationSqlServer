using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Messages.Commands
{
    public abstract class CommandBase : MessageBase, ICommand
    {
        protected override string Prefix => "cmd";
    }

    public abstract class CommandBase<TInput> : MessageBase<TInput>, ICommand<TInput>
    {
        protected override string Prefix => "cmd";
    }

    public abstract class CommandBase<TInput, TValidator> : MessageBase<TInput, TValidator>, ICommand<TInput, TValidator>
        where TValidator : IValidator<TInput>, new()
    {
        protected override string Prefix => "cmd";
    }
}