using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.SqlServer.UnitTests
{
    public static class TestHelper
    {
        public static Stream GetFileFromResources(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(fileName));

            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) throw new Exception($"Missing {resourceName}");

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public static string GetTextFromFileFromResources(string fileName)
        {
            var stream = GetFileFromResources(fileName);

            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}