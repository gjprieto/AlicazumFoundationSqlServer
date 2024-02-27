using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security.CSharp
{
    public class CustomCSharpMigrationOperationGenerator(CSharpMigrationOperationGeneratorDependencies dependencies) : CSharpMigrationOperationGenerator(dependencies)
    {
        private ICSharpHelper Code => dependencies.CSharpHelper;

        protected override void Generate(CreateTableOperation operation, IndentedStringBuilder builder)
        {
            base.Generate(operation, builder);

            builder.AppendLine(".CreateTable(");

            using (builder.Indent())
            {
                builder
                    .Append("name: ")
                    .Append(Code.Literal(operation.Name))
                    .AppendLine(",")
                    .Append("schema: ")
                    .Append(Code.Literal("security"))
                    .AppendLine(",");

                builder
                    .AppendLine("columns: table => new")
                    .AppendLine("{");

                builder.AppendLine("}");
            }

            builder.AppendLine(");");
        }
    }
}