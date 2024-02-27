using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext
{
    public interface IDbSecurityContextProvider
    {
        string GetUserId();
        string[] GetUserRoles();
        string GetUserNameOrProcess();
    }
}