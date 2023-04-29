using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EsotericDevZone.Core.Reflection
{
    public static class StaticTypesExtensions
    {
        public static IEnumerable<I> EnumeratePublicStaticProperties<I>(this Type type)
                => type.GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(p => p.PropertyType == typeof(I))
                    .Select(p => (I)p.GetValue(null));
    }
}
