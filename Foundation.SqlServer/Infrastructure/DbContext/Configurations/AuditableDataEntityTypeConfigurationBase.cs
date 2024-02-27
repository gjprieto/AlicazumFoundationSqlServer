using Foundation.SqlServer.Infrastructure.DbContext.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.Configurations
{
    public abstract class AuditableDataEntityTypeConfigurationBase<TEntity, TId> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IAuditableDataEntity<TId>
    {
        public abstract string TableName { get; }

        protected abstract void DoEntityConfiguration(EntityTypeBuilder<TEntity> builder);

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName, x => x.IsTemporal())                
                .HasKey(c => c.Id);

            builder.Property(c => c.IsDeteled).HasDefaultValue(false).IsRequired();
            builder.Property(c => c.UserOrNameProcess).IsRequired();

            DoEntityConfiguration(builder);

            builder.HasQueryFilter(c => c.IsDeteled == false);
        }
    }
}