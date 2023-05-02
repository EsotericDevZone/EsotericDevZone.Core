using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EsotericDevZone.Core
{    
    public static partial class Extensions
    {
        public static IEnumerable<I> EnumeratePublicStaticProperties<I>(this Type type)
                => type.GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(p => p.PropertyType == typeof(I))
                    .Select(p => (I)p.GetValue(null));
    }
}
