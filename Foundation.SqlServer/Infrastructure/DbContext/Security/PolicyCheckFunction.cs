using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text.Json.Serialization;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security
{
    public class PolicyCheckFunction
    {
        public static string AnnotationPrefix => "securitycheckfunction";

        public string FunctionSchema { get; set; } = "security";
        public required string FunctionName { get; set; }
        public required string PolicyTableSchema { get; set; }
        public required string PolicyTableName { get; set; }
        public string EntityIdParameterName { get; set; } = "EntityId";
        public string EntityIdParameterType { get; set; } = "nvarchar(75)";
        public string UserIdSessionContextName { get; set; } = "UserId";
        public string UserIdColumnName { get; set; } = "UserId";
        public string UserIdColumnType { get; set; } = "nvarchar(75)";
        public string EntityIdColumnName { get; set; } = "EntityId";
        public string PolicyColumnName { get; set; } = "Policy";

        [JsonIgnore]
        public string Annotation => $"{AnnotationPrefix}:{FunctionName}";

        public static PolicyCheckFunction? FromJsonString(string? json)
        {
            if (string.IsNullOrEmpty(json)) return null;
            return System.Text.Json.JsonSerializer.Deserialize<PolicyCheckFunction>(json);
        }

        public string ToJson() => System.Text.Json.JsonSerializer.Serialize(this);

        public string RenderCreateSql()
        {
            return $@"
                CREATE FUNCTION [{FunctionSchema}].[{FunctionName}](@{EntityIdParameterName} {EntityIdParameterType}, @Policy nvarchar(50))
                RETURNS TABLE  
                    WITH SCHEMABINDING  
                AS
	            RETURN
		            SELECT 1 AS Result
		            FROM [{PolicyTableSchema}].[{PolicyTableName}]
		            WHERE [{UserIdColumnName}] = CAST(SESSION_CONTEXT(N'{UserIdSessionContextName}') AS {UserIdColumnType}) AND [{EntityIdColumnName}] = @{EntityIdParameterName} AND [{PolicyColumnName}] = @Policy;            
            ";
        }
    }
}