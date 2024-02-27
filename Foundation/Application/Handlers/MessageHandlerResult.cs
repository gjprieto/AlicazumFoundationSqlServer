using Foundation.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Handlers
{
    public class MessageHandlerResult : IMessageHandlerResult
    {
        public bool IsValid { get; }
        public bool IsCompleted { get; }
        public string[] Warnings { get; }
        public string[] Errors { get; }

        private MessageHandlerResult(bool isValid, bool isCompleted, string[] warnings, string[] errors)
        {
            IsValid = isValid;
            IsCompleted = isCompleted;
            Warnings = warnings;
            Errors = errors;
        }

        public static IMessageHandlerResult GetWarningResult(string[] warnings) => new MessageHandlerResult(false, false, warnings, new string[0]);
        public static IMessageHandlerResult GetNotValidResult(string error) => new MessageHandlerResult(false, false, new string[0], new string[1] { error });
        public static IMessageHandlerResult GetNotValidResult(string[] errors) => new MessageHandlerResult(false, false, new string[0], errors);
        public static IMessageHandlerResult GetErrorResult(string error) => new MessageHandlerResult(true, false, new string[0], new string[1] { error });
        public static IMessageHandlerResult GetErrorResult(string[] errors) => new MessageHandlerResult(true, false, new string[0], errors);
        public static IMessageHandlerResult GetOkResult() => new MessageHandlerResult(true, true, new string[0], new string[0]);
    }

    public class MessageHandlerResult<TResult> : IMessageHandlerResult<TResult>
    {
        public bool IsValid { get; }
        public bool IsCompleted { get; }
        public string[] Warnings { get; }
        public string[] Errors { get; }
        public TResult? Result { get; }

        private MessageHandlerResult(bool isValid, bool isCompleted, string[] warnings, string[] errors, TResult? result)
        {
            IsValid = isValid;
            IsCompleted = isCompleted;
            Warnings = warnings;
            Errors = errors;
            Result = result;
        }

        public static IMessageHandlerResult<TResult> GetWarningResult(string[] warnings) => new MessageHandlerResult<TResult>(false, false, warnings, new string[0], default);
        public static IMessageHandlerResult<TResult> GetNotValidResult(string error) => new MessageHandlerResult<TResult>(false, false, new string[0], new string[1] { error }, default);
        public static IMessageHandlerResult<TResult> GetNotValidResult(string[] errors) => new MessageHandlerResult<TResult>(false, false, new string[0], errors, default);
        public static IMessageHandlerResult<TResult> GetErrorResult(string error) => new MessageHandlerResult<TResult>(true, false, new string[0], new string[1] { error }, default);
        public static IMessageHandlerResult<TResult> GetErrorResult(string[] errors) => new MessageHandlerResult<TResult>(true, false, new string[0], errors, default);
        public static IMessageHandlerResult<TResult> GetOkResult(TResult result) => new MessageHandlerResult<TResult>(true, true, new string[0], new string[0], result);
    }
}