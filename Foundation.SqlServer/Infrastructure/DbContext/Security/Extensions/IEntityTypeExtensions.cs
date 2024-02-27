using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security.Extensions
{
    public static class IEntityTypeExtensions
    {
        public static PolicySchema? GetPolicySchema(this IEntityType entityType)
        {
            var annotation = entityType.FindAnnotation(PolicySchema.Annotation);
            if (annotation == null || annotation.Value == null) return null;

            return PolicySchema.FromJsonString(annotation?.Value.ToString());
        }

        public static PolicyTable? GetPolicyTable(this IEntityType entityType)
        {
            var annotation = entityType.FindAnnotation(PolicyTable.Annotation);
            if (annotation == null || annotation.Value == null) return null;

            return PolicyTable.FromJsonString(annotation?.Value.ToString());
        }

        public static List<PolicyCheckFunction>? GetPolicyCheckFunctions(this IEntityType entityType)
        {
            var annotations = entityType.GetAnnotations();
            if (annotations == null) return null;

            var functions = new List<PolicyCheckFunction>();

            foreach (var annotation in annotations)
            {
                if (annotation.Name.StartsWith(PolicyCheckFunction.AnnotationPrefix))
                {
                    if (annotation.Value == null) continue;
                    functions.Add(PolicyCheckFunction.FromJsonString(annotation.Value.ToString()));
                }
            }

            return functions;
        }

        public static SecurityPolicy? GetSecurityPolicy(this IEntityType entityType)
        {
            var annotation = entityType.FindAnnotation(SecurityPolicy.Annotation);
            if (annotation == null || annotation.Value == null) return null;

            return SecurityPolicy.FromJsonString(annotation?.Value.ToString());
        }
    }
}