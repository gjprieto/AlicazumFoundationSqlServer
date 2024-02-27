using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.DataEntities
{
    public abstract class DataEntityBase<TId> : IDataEntity<TId>
    {
        public TId Id { get; set; }
    }
}