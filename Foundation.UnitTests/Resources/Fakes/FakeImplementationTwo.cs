using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.UnitTests.Resources.Fakes
{
    internal class FakeImplementationTwo : IFakeInterface<int, int>
    {
        public int Value1 => 1;
        public int Value2 => 2;
    }
}