using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Serialization;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security
{
    public class PolicyTable
    {
        public static string Annotation => "policytable";

        public string Schema { get; set; } = "security";
        public required string Name { get; set; }
        public string UserIdColumnName { get; set; } = "UserId";
        public string UserIdColumnType { get; set; } = "nvarchar(75)";
        public string EntityIdColumnName { get; set; } = "EntityId";
        public string EntityIdColumnType { get; set; } = "nvarchar(75)";
        public string PolicyColumnName { get; set; } = "Policy";

        public static PolicyTable? FromJsonString(string json) => System.Text.Json.JsonSerializer.Deserialize<PolicyTable>(json);

        public string ToJson() => System.Text.Json.JsonSerializer.Serialize(this);

        public string RenderCreateSql()
        {
            return $@"
                CREATE TABLE [{Schema}].[{Name}](
                    [{EntityIdColumnName}] {EntityIdColumnType} NOT NULL,
                    [{UserIdColumnName}] {UserIdColumnType} NOT NULL,
                    [{PolicyColumnName}] nvarchar(50) NOT NULL,
                    CONSTRAINT [PK_{Name}] PRIMARY KEY CLUSTERED ([{EntityIdColumnName}], [{UserIdColumnName}], [{PolicyColumnName}]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) 
                ON [PRIMARY];
            ";
        }

        public string RenderInserPolicySql() 
        {
            return $@"
                INSERT INTO [{Schema}].[{Name}] ([{EntityIdColumnName}], [{UserIdColumnName}], [{PolicyColumnName}])
                VALUES (@EntityId, @UserId, @Policy);
            ";
        }
    }
}