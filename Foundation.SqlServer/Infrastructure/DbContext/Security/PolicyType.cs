using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Foundation.SqlServer.Infrastructure.DbContext.Security
{
    public sealed class PolicyType
    {
        public string Policy { get; set; }

        [JsonConstructor]
        private PolicyType(string policy)
        {
            Policy = policy.ToLower();
        }

        public static PolicyType Read => new PolicyType("read");
        public static PolicyType Write => new PolicyType("write");
        public static PolicyType Delete => new PolicyType("delete");
        public static PolicyType Owner => new PolicyType("owner");
    }
}