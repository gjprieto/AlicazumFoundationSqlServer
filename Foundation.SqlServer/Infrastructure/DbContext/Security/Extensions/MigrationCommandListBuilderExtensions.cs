using Microsoft.EntityFrameworkCore.Migrations;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security.Extensions
{
    public static class MigrationCommandListBuilderExtensions
    {
        public static void AppendCreateSchemaIfDoesNotExists(this MigrationCommandListBuilder builder, string schema)
        {
            builder.Append(@$"
                IF NOT EXISTS ( SELECT * FROM sys.schemas WHERE name = N'{schema}' )
                    EXEC('CREATE SCHEMA [{schema}] AUTHORIZATION [dbo]');
            ")
            .EndCommand();
        }

        public static void AppendCreatePolicyTable(this MigrationCommandListBuilder builder, PolicyTable table)
        {
            builder.Append(table.RenderCreateSql()).EndCommand();
        }

        public static void AppendStartSecurityPolicy(this MigrationCommandListBuilder builder, string schema, string name)
        {
            builder.Append($"CREATE SECURITY POLICY [{schema}].[{name}]");
        }

        public static void AppendAddFilterPredicate(this MigrationCommandListBuilder builder, string schema, string function, string column, string table)
        {
            builder.Append($"ADD FILTER PREDICATE [{schema}].[{function}]({column}) ON [dbo].[{table}],");
        }

        public static void AppendEndSecurityPolicy(this MigrationCommandListBuilder builder, string schema, string name)
        {
            builder.Append($"WITH (STATE = ON);");
        }
    }
}