using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task AddPolicy<TEntity>(this Microsoft.EntityFrameworkCore.DbContext context, string userId, object entityId, PolicyType policy)
        {
            var type = typeof(TEntity);

            var entityType = context.Model.FindEntityType(type);
            if (entityType == null) throw new Exception($"No EntityType for {type.Name}");

            var policyTable = entityType.GetPolicyTable();
            if (policyTable == null) throw new Exception($"Entity {type.Name} has no policy table");

            object[] parameters = new object[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@EntityId", entityId),
                new SqlParameter("@Policy", policy.Policy)
            };

            await context.Database.ExecuteSqlRawAsync(policyTable.RenderInserPolicySql(), parameters);
        }
    }
}