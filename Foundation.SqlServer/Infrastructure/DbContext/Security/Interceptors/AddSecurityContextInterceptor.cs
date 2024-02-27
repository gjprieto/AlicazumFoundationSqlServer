using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security.Interceptors
{
    public class AddSecurityContextInterceptor(IDbSecurityContextProvider dbSecurityContextProvider) : DbCommandInterceptor
    {
        private readonly IDbSecurityContextProvider _dbSecurityContextProvider = dbSecurityContextProvider;

        private DbCommand AddSecurityContextToCommand(DbCommand command)
        {
            var securityContext = new SecurityContext { UserId = _dbSecurityContextProvider.GetUserId(), UserRoles = _dbSecurityContextProvider.GetUserRoles().ToList() };            

            command.CommandText = securityContext.RenderExecuteSql() + command.CommandText;

            return command;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            command = AddSecurityContextToCommand(command);
            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            command = AddSecurityContextToCommand(command);
            return base.ReaderExecuting(command, eventData, result);
        }

        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            command = AddSecurityContextToCommand(command);
            return base.NonQueryExecuting(command, eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            command = AddSecurityContextToCommand(command);
            return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
        }

        public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {
            command = AddSecurityContextToCommand(command);
            return base.ScalarExecuting(command, eventData, result);
        }

        public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
        {
            command = AddSecurityContextToCommand(command);
            return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
        }
    }
}