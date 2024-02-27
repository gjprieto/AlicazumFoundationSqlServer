using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.UnitTests.Resources.Fakes
{
    public class FakeImplementationOne : IFakeInterface<string, string>
    {
        public string Value1 => "1";
        public string Value2 => "2";
    }
}