using Foundation.SqlServer.Infrastructure.DbContext.Interceptors;
using Foundation.SqlServer.Infrastructure.DbContext.Security.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext
{
    public abstract class DataEntityDbContextBase(IOptions<IDataEntityDbContextConfiguration> options, IDbSecurityContextProvider securityProvider) : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IDataEntityDbContextConfiguration _options = options.Value;

        public abstract bool HasAuditTrail { get; }
        public abstract bool HasPolicyChecks { get; }

        protected IDbSecurityContextProvider DbSecurityContextProvider => securityProvider;

        protected abstract void DoConfiguration(DbContextOptionsBuilder optionsBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options.ConnectionString);

            if (HasAuditTrail) 
            {
                SoftDeleteInterceptor softdeleteInterceptor = new(securityProvider);
                optionsBuilder.AddInterceptors(softdeleteInterceptor);
            }

            if (HasPolicyChecks) 
                optionsBuilder.ReplaceService<IMigrationsSqlGenerator, CustomSqlServerMigrationsSqlGenerator>();

            DoConfiguration(optionsBuilder);
        }
    }
}