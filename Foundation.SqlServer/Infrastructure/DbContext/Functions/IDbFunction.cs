using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.Functions
{
    public interface IDbFunction
    {
        string Schema { get; }
        string Name { get; }

        string Render();
    }
}