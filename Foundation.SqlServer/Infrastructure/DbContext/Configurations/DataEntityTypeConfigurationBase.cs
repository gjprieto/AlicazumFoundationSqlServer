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
    public abstract class DataEntityTypeConfigurationBase<TEntity, TId> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IDataEntity<TId>
    {
        public abstract string TableName { get; }

        protected abstract void DoEntityConfiguration(EntityTypeBuilder<TEntity> builder);

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName).HasKey(c => c.Id);
            DoEntityConfiguration(builder);
        }
    }
}