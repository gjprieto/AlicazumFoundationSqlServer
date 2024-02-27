using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Messages
{
    public interface IMessageValidationResult
    {
        bool IsValid { get; }
        bool IsNotValid { get; }
        string[] Warnings { get; }
        string[] Errors { get; }
    }
}