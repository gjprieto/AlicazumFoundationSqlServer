using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.Application.Messages.Queries
{
    public abstract class QueryBase : MessageBase, IQuery
    {
        protected override string Prefix => "qry";
    }

    public abstract class QueryBase<TInput> : MessageBase<TInput>, IQuery<TInput>
    {
        protected override string Prefix => "qry";
    }

    public abstract class QueryBase<TInput, TValidator> : MessageBase<TInput, TValidator>, IQuery<TInput, TValidator>
        where TValidator : IValidator<TInput>, new()
    {
        protected override string Prefix => "qry";
    }
}