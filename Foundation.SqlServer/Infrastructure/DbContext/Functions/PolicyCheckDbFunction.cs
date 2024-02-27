using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.Functions
{
    public abstract class PolicyCheckDbFunctionBase(string schema, string functionName) : IDbFunction
    {
        public string Schema => schema;
        public string Name => functionName;

        protected abstract string BitColumnNameToCheck { get; }

        public string Render()
        {
            return $@"
                CREATE FUNCTION [{Schema}].[{Name}](@EntityId nvarchar(75))
                    RETURNS TABLE  
                    WITH SCHEMABINDING
                AS
	                RETURN 
		                SELECT Count(*) AS Result
		                FROM [security].[Policies]
		                WHERE [UserId] = CAST(SESSION_CONTEXT(N'UserId') AS nvarchar(75)) AND [EntityId] = @EntityId AND [{BitColumnNameToCheck}] = 1;
                GO
            ";
        }
    }
}
