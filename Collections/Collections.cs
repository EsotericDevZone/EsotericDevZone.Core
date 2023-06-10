using System;
using System.Collections.Generic;
using System.Linq;

namespace EsotericDevZone.Core.Collections
{    
    public static class Lists
    {
        public static List<T> Of<T>(params T[] items) => items.ToList();
        public static List<T> Empty<T>() => new List<T>();
        public static List<T> UniformValued<T>(T item, int count) => Enumerable.Repeat(item, count).ToList();

        public static List<int> Range(int start, int count) => Enumerable.Range(start, count).ToList();

        public static List<T> Generate<T>(Func<T> generator, int count)
            => Enumerable.Repeat(0, count).Select(_=>generator()).ToList();

        public static List<T> Generate<T>(Func<int, T> generator, int count)
            => Enumerable.Range(0, count).Select(generator).ToList();
    }

    public static class Arrays
    {
        public static T[] Of<T>(params T[] items) => items.ToArray();
        public static T[] Empty<T>() => new T[0];
        public static T[] UniformValued<T>(T item, int count) => Enumerable.Repeat(item, count).ToArray();
        public static int[] Range(int start, int count) => Enumerable.Range(start, count).ToArray();
        public static T[] Generate<T>(Func<T> generator, int count)
            => Enumerable.Repeat(0, count).Select(_ => generator()).ToArray();

        public static T[] Generate<T>(Func<int, T> generator, int count)
            => Enumerable.Range(0, count).Select(generator).ToArray();
    }
}
