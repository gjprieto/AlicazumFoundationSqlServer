using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore;
using Foundation.SqlServer.Infrastructure.DbContext.Security.Extensions;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security.SqlServer
{
    public class CustomSqlServerMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies, ICommandBatchPreparer commandBatchPreparer) : SqlServerMigrationsSqlGenerator(dependencies, commandBatchPreparer)
    {
        private static IEntityType? GetEntityTypeByTable(IModel model, string tableName)
        {
            foreach (var entityType in model.GetEntityTypes())
                if (entityType.GetTableName() == tableName) return entityType;

            return null;
        }

        protected override void Generate(CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder, bool terminate = true)
        {
            base.Generate(operation, model, builder, terminate);

            if (model == null) return;

            var entityType = GetEntityTypeByTable(model, operation.Name);
            if (entityType == null) return;

            var policySchema = entityType.GetPolicySchema();
            if (policySchema != null) builder.Append(policySchema.RenderCreateSql()).EndCommand();

            var policyTable = entityType.GetPolicyTable();
            if (policyTable != null) builder.Append(policyTable.RenderCreateSql()).EndCommand();

            var policyCheckFunctions = entityType.GetPolicyCheckFunctions();
            policyCheckFunctions?.ForEach(x => builder.Append(x.RenderCreateSql()).EndCommand());

            var securityTable = entityType.GetSecurityPolicy();
            if (securityTable != null) builder.Append(securityTable.RenderCreateSql()).EndCommand();
        }
    }
}