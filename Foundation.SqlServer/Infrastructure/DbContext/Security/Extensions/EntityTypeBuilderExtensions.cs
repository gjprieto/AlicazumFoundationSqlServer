using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<TEntity> HasPolicySchema<TEntity>(this EntityTypeBuilder<TEntity> builder, PolicySchema schema)
            where TEntity : class
        {
            builder.HasAnnotation(PolicySchema.Annotation, schema.ToJson());
            return builder;
        }

        public static EntityTypeBuilder<TEntity> HasPolicyTable<TEntity>(this EntityTypeBuilder<TEntity> builder, PolicyTable table)
            where TEntity : class
        {
            builder.HasAnnotation(PolicyTable.Annotation, table.ToJson());
            return builder;
        }

        public static EntityTypeBuilder<TEntity> HasPolicyCheckFunction<TEntity>(this EntityTypeBuilder<TEntity> builder, PolicyCheckFunction function)
            where TEntity : class
        {
            builder.HasAnnotation(function.Annotation, function.ToJson());
            return builder;
        }

        public static EntityTypeBuilder<TEntity> HasSecurityPolicy<TEntity>(this EntityTypeBuilder<TEntity> builder, SecurityPolicy policy)
            where TEntity : class
        {
            builder.HasAnnotation(SecurityPolicy.Annotation, policy.ToJson());
            return builder;
        }
    }
}