using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.DataEntities
{
    public interface IAuditableDataEntity
    {
        string? UserOrNameProcess { get; set; }
        bool IsDeteled { get; set; }
    }

    public interface IAuditableDataEntity<TId> : IAuditableDataEntity, IDataEntity<TId>
    {
    }
}