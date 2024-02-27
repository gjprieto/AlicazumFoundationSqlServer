using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.DataEntities
{
    public class AuditableDataEntityBase<TId> : DataEntityBase<TId>, IAuditableDataEntity<TId>
    {
        public string? UserOrNameProcess { get; set; }
        public bool IsDeteled { get; set; }
    }
}