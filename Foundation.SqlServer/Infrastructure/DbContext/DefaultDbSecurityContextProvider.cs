using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext
{
    public abstract class DbSecurityContextProviderBase : IDbSecurityContextProvider
    {
        public abstract string GetUserId();

        public abstract string[] GetUserRoles();

        public abstract string GetUserNameOrProcess();
    }
}