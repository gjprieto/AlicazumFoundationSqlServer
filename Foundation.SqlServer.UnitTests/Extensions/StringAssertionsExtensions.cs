using FluentAssertions.Execution;
using FluentAssertions.Numeric;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Primitives;
using System.Globalization;

namespace Foundation.SqlServer.UnitTests.Extensions
{
    public static class StringAssertionsExtensions
    {
        public static AndConstraint<StringAssertions<FluentAssertions.Primitives.StringAssertions>> EqualsIgnoringCaseAndSymbols(this StringAssertions<FluentAssertions.Primitives.StringAssertions> parent, string targetValue, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .Given(() => parent.Subject)
                .ForCondition(result => String.Compare(result, targetValue, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0)
                .FailWith($"Not equal");

            return new AndConstraint<StringAssertions<FluentAssertions.Primitives.StringAssertions>>(parent);
        }
    }
}