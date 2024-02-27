using FluentAssertions;
using Foundation.SqlServer.Infrastructure.DbContext.Security;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.UnitTests.Infrastructure.DbContext.Security
{
    public class PolicySchemaTheories
    {
        public static IEnumerable<object[]> RenderCreateSqlTestParameters()
        {
            return new List<object[]> {
                new object[] { new PolicySchema(), TestHelper.GetTextFromFileFromResources("CreatePolicySchema_default.sql").Trim() },
                new object[] { new PolicySchema { Name = "custom" }, TestHelper.GetTextFromFileFromResources("CreatePolicySchema_custom.sql").Trim() }
            };
        }

        [Theory(DisplayName = "PolicySchema RenderCreateSql Tests")]
        [MemberData(nameof(RenderCreateSqlTestParameters))]
        public void RenderCreateSqlMethod(PolicySchema policySchema, string expectedSql)
        {
            var resultSql = policySchema.RenderCreateSql();

            resultSql.Trim().Should().EqualsIgnoringCaseAndSymbols(expectedSql);
        }
    }
}