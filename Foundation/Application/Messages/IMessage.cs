using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Messages
{
    public interface IMessage
    {
        string Id { get; }

        Task<IMessageValidationResult> ValidateAsync();
    }

    public interface IMessage<TInput> : IMessage
    {
        TInput Input { get; set; }
    }

    public interface IMessage<TInput, TValidator> : IMessage<TInput>
        where TValidator: IValidator<TInput>
    {
        TValidator Validator { get; }        
    }
}