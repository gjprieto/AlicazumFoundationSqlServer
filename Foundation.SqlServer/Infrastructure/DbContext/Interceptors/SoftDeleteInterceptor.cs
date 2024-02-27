using Foundation.SqlServer.Infrastructure.DbContext.DataEntities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.Interceptors
{
    public class SoftDeleteInterceptor(IDbSecurityContextProvider dbSecurityContextProvider) : SaveChangesInterceptor
    {
        private readonly IDbSecurityContextProvider _dbSecurityContextProvider = dbSecurityContextProvider;

        private void UpdateEntityInsteadOfDeletion(EntityEntry<IAuditableDataEntity>? entity) 
        { 
            if (entity == null) return;

            if (entity.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
            {
                entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                entity.Property(nameof(IAuditableDataEntity.UserOrNameProcess)).CurrentValue = _dbSecurityContextProvider.GetUserNameOrProcess();
                entity.Property(nameof(IAuditableDataEntity.IsDeteled)).CurrentValue = true;
            }
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var entities = eventData?.Context?.ChangeTracker
                .Entries<IAuditableDataEntity>()
                .Where(state => state.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
                .ToList();

            if (entities != null && entities.Any()) 
            { 
                foreach( var entity in entities) 
                {
                    UpdateEntityInsteadOfDeletion(entity);
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}