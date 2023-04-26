using System;
using System.Collections.Generic;
using System.Linq;

namespace EsotericDevZone.Core.Collections
{
    public static class Extensions
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> lists) => lists.SelectMany(_ => _);
    }
}
