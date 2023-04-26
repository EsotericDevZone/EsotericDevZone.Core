using System.Collections.Generic;
using System.Linq;

namespace EsotericDevZone.Core.Collections
{
    public static class Collections
    {
        public static List<T> ListOf<T>(params T[] items) => items.ToList();        
        public static T[] ArrayOf<T>(params T[] items) => items.ToArray();

        public static List<T> EmptyList<T>() => new List<T>();
        public static T[] EmptyArray<T>() => new T[0];

    }
}
