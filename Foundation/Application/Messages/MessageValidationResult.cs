using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Messages
{
    public class MessageValidationResult : IMessageValidationResult
    {
        public bool IsValid { get; }
        public bool IsNotValid => !IsValid;
        public string[] Warnings { get; }
        public string[] Errors { get; }

        private MessageValidationResult(bool isValid, string[] warnings, string[] errors) 
        { 
            IsValid = isValid;
            Warnings = warnings;
            Errors = errors;
        }

        public static IMessageValidationResult GetWarningResult(string[] warnings) => new MessageValidationResult(false, warnings, new string[0]);
        public static IMessageValidationResult GetErrorResult(string[] errors) => new MessageValidationResult(false, new string[0], errors);
        public static IMessageValidationResult GetOkResult() => new MessageValidationResult(true, new string[0], new string[0]);
    }
}