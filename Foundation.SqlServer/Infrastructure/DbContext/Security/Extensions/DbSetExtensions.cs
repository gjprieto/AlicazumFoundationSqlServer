using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security.Extensions
{
    public static class DbSetExtensions
    {
        public static DbCommand CreateDbCommand<TEntity>(this DbSet<TEntity> dbSet, string sql)
            where TEntity : class
        {
            var command = dbSet.CreateDbCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;

            return command;
        }


        public static async Task AddPolicyAsync<TEntity, TId>(this DbSet<TEntity> dbSet, string userId, TId entityId, string policy)
            where TEntity : class
        {   
            var policyTable = dbSet.EntityType.GetPolicyTable();
            if (policyTable == null) throw new Exception($"Entity {typeof(TEntity).Name} has no policy table");

            using (var command = dbSet.CreateDbCommand(policyTable.RenderInserPolicySql())) 
            {
                command.AddParameter("UserId", userId);
                command.AddParameter("EntityId", entityId);
                command.AddParameter("Policy", policy);

                await command.ExecuteNonQueryAsync();
            }                
        }
    }
}