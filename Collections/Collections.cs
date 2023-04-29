using System.Collections.Generic;
using System.Linq;

namespace EsotericDevZone.Core.Collections
{    
    public static class Lists
    {
        public static List<T> Of<T>(params T[] items) => items.ToList();
        public static List<T> Empty<T>() => new List<T>();
    }

    public static class Arrays
    {
        public static T[] Of<T>(params T[] items) => items.ToArray();
        public static T[] Empty<T>() => new T[0];
    }
}
