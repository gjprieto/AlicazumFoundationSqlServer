using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.UnitTests.Resources.Fakes
{
    public interface IFakeInterface<T1, T2>
    {
        T1 Value1 { get; }
        T2 Value2 { get; }
    }
}
