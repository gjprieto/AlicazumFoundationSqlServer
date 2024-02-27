using Foundation.Application.Messages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Handlers
{
    public abstract class MessageHandlerBase     
    {
        private readonly ILogger _logger;

        protected MessageHandlerBase(ILogger logger)
        {
            _logger = logger;
        }

        protected virtual Dictionary<string, object> GetLogScope(IMessage message)
        {
            var scope = new Dictionary<string, object>
            {
                ["MessageId"] = message.Id,
                ["MessageType"] = message.GetType().Name,
                ["MessageHandlerType"] = this.GetType().Name,
                ["MessageHandlingOn"] = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss:fff")
            };

            return scope;
        }

        protected virtual IDisposable? LogBeginMessageScope(IMessage message) => _logger.BeginScope(message);
        protected virtual void LogInformation(string message) => _logger.LogInformation(message);
        protected virtual void LogError(string errorMessage) => _logger.LogError(errorMessage);
        protected virtual void LogErrors(string[] errorMessages) => errorMessages.ToList().ForEach(error => _logger.LogError(error));
        protected virtual void LogException(Exception ex) => _logger.LogCritical(ex, ex.Message);

        protected async Task<IMessageValidationResult> ValidateMessageAsync(IMessage message)
        {
            LogInformation($"Validating message {message.Id}");

            var messageValidation = await message.ValidateAsync() ?? throw new InvalidOperationException($"Null validation result when validation message {message.GetType().Name}");

            if (messageValidation.IsNotValid)
            {
                LogErrors(messageValidation.Errors);
            }

            return messageValidation;
        }        
    }

    public abstract class MessageHandlerBase<TMessage> : MessageHandlerBase, IMessageHandler<TMessage> 
        where TMessage : IMessage
    {
        protected MessageHandlerBase(ILogger logger) : base(logger)
        {
        }

        protected IMessageHandlerResult Ok() => MessageHandlerResult.GetOkResult();
        protected IMessageHandlerResult NotValid(string[] errors) => MessageHandlerResult.GetNotValidResult(errors);
        protected IMessageHandlerResult Warning(string[] warnings) => MessageHandlerResult.GetWarningResult(warnings);
        protected IMessageHandlerResult Error(Exception ex) => MessageHandlerResult.GetErrorResult(new string[] { ex.Message });
        protected IMessageHandlerResult Error(string[] errors) => MessageHandlerResult.GetErrorResult(errors);

        protected abstract Task<IMessageValidationResult> ValidateHandlerAsync(TMessage message);
        protected abstract Task<IMessageHandlerResult> DoHandleAsync(TMessage message);

        public async Task<IMessageHandlerResult> HandleAsync(TMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            using (LogBeginMessageScope(message)) 
            {   
                var messageValidation = await ValidateMessageAsync(message);
                if (messageValidation.IsNotValid) return NotValid(messageValidation.Errors);                

                var handlerValidation = await ValidateHandlerAsync(message);
                if (handlerValidation.IsNotValid) return NotValid(handlerValidation.Errors);

                try
                {
                    return await DoHandleAsync(message);
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    return Error(ex);
                }
            }
        }
    }

    public abstract class MessageHandlerBase<TMessage, TResult> : MessageHandlerBase, IMessageHandler<TMessage, TResult>
        where TMessage : IMessage
    {
        protected MessageHandlerBase(ILogger logger) : base(logger)
        {
        }

        protected IMessageHandlerResult<TResult> Ok(TResult result) => MessageHandlerResult<TResult>.GetOkResult(result);
        protected IMessageHandlerResult<TResult> NotValid(string[] errors) => MessageHandlerResult<TResult>.GetNotValidResult(errors);
        protected IMessageHandlerResult<TResult> Warning(string[] warnings) => MessageHandlerResult<TResult>.GetWarningResult(warnings);
        protected IMessageHandlerResult<TResult> Error(Exception ex) => MessageHandlerResult<TResult>.GetErrorResult(new string[] { ex.Message });
        protected IMessageHandlerResult<TResult> Error(string[] errors) => MessageHandlerResult<TResult>.GetErrorResult(errors);

        protected abstract Task<IMessageValidationResult> ValidateHandlerAsync(TMessage message);
        protected abstract Task<IMessageHandlerResult<TResult>> DoHandleAsync(TMessage message);

        public async Task<IMessageHandlerResult<TResult>> HandleAsync(TMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            using (LogBeginMessageScope(message))
            {
                var messageValidation = await ValidateMessageAsync(message);
                if (messageValidation.IsNotValid) return NotValid(messageValidation.Errors);

                var handlerValidation = await ValidateHandlerAsync(message);
                if (handlerValidation.IsNotValid) return NotValid(handlerValidation.Errors);

                try
                {
                    return await DoHandleAsync(message);
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    return Error(ex);
                }
            }
        }
    }
}