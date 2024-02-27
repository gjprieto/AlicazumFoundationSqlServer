namespace Foundation.SqlServer.Infrastructure.DbContext.Security
{
    public class PolicySchema
    {
        public static string Annotation => "policyschema";

        public string Name { get; set; } = "security";

        public static PolicySchema? FromJsonString(string json) => System.Text.Json.JsonSerializer.Deserialize<PolicySchema>(json);

        public string ToJson() => System.Text.Json.JsonSerializer.Serialize(this);

        public string RenderCreateSql()
        {
            return $@"
                IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'{Name}')
                    EXEC('CREATE SCHEMA [{Name}] AUTHORIZATION [dbo]');
            ";
        }
    }
}