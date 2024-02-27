using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.DataEntities
{
    public class PolicyDataEntity
    {
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public bool CanSelect { get; set; }
        public bool CanDelete { get; set; }
        public bool CanInsert { get; set; }
        public bool CanUpdate { get; set; }
    }
}