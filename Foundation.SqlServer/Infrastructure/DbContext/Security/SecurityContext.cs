using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security
{
    public class SecurityContext
    {
        public required string UserId { get; set; }
        public List<string> UserRoles { get; set; } = new List<string>();

        public string RenderExecuteSql()
        {
            return $@"
                EXEC SP_SET_SESSION_CONTEXT @key=N'{nameof(UserId)}', @value='{UserId}';
                EXEC SP_SET_SESSION_CONTEXT @key=N'{nameof(UserRoles)}', @value='{string.Join(",", UserRoles.Select(x => $"[{x}]").ToArray())}';
            ";
        }
    }
}