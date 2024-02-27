using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.UnitTests.Resources.Fakes
{
    public static class Constants
    {
        public const string NAME_EMPTY = "Name cannot be empty";
        public const string NAME_TOO_LONG = "[MaximumLengthValidator] - Name is too long (max 10 chars)";
        public const string BAD_VALUE_RANGE = "Value must be int between 10 and 20";
        public const string ERROR_VALUE_15 = "Value cannot be 15";
        public const string ERROR_VALUE_16 = "Value cannot be 16";
    }
}