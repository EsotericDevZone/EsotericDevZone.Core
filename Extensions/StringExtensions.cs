using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsotericDevZone.Core
{
    public static partial class Extensions
    {
        public static string Indent(this string str, string beforeLine = "    ")
        {
            return str.Split('\n').Select(line => beforeLine + line).JoinToString("\n");
        }

        public static string JoinToString<T>(this IEnumerable<T> values, string separator)
            => string.Join(separator, values);
    }
}
