using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Messages
{
    public abstract class MessageBase : IMessage
    {
        public string Id { get; }

        protected abstract string Prefix { get; }

        protected MessageBase() 
        {
            Id = $"{Prefix}-{Guid.NewGuid()}";
        }

        public abstract Task<IMessageValidationResult> ValidateAsync();
    }

    public abstract class MessageBase<TInput> : MessageBase, IMessage<TInput>
    {
        public required TInput Input { get; set; }
    }

    public abstract class MessageBase<TInput, TValidator> : MessageBase<TInput>, IMessage<TInput, TValidator>
        where TValidator : IValidator<TInput>, new()
    {
        public virtual TValidator Validator => new();

        public override async Task<IMessageValidationResult> ValidateAsync()
        {
            var validationResult = await Validator.ValidateAsync(Input);
            if (validationResult.IsValid) return MessageValidationResult.GetOkResult();

            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray();

            return MessageValidationResult.GetErrorResult(errors);
        }
    }
}