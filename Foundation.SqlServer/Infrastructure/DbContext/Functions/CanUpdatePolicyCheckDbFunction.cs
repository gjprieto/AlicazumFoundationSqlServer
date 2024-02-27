using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.Functions
{
    public class CanUpdatePolicyCheckDbFunction(string schema, string functionName) : PolicyCheckDbFunctionBase(schema, functionName)
    {
        protected override string BitColumnNameToCheck => "CanUpdate";
    }
}
