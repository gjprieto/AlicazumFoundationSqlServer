using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Messages.Queries
{
    public interface IQuery : IMessage
    {
    }

    public interface IQuery<TInput> : IMessage<TInput>, IQuery
    {
    }

    public interface IQuery<TInput, TValidator> : IMessage<TInput, TValidator>, IQuery<TInput>
        where TValidator : IValidator<TInput>
    {
    }
}