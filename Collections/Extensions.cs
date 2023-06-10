using System;
using System.Collections.Generic;
using System.Linq;

namespace EsotericDevZone.Core.Collections
{
    public static class Extensions
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> lists) => lists.SelectMany(_ => _);

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> values) => values.OrderBy(_ => Guid.NewGuid());


        #region ArgMin/ArgMax
        public static int ArgMin<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer = null)
        {
            Validation.ThrowIf(source.Count() == 0, "Source is empty");
            comparer = comparer ?? Comparer<TKey>.Default;
            return source
                .Select((x, i) => (key: selector(x), pos: i))
                .Aggregate((x, y) => comparer.Compare(x.key, y.key) < 0 ? x : y).pos;
        }

        public static int ArgMax<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer = null)
        {
            Validation.ThrowIf(source.Count() == 0, "Source is empty");
            comparer = comparer ?? Comparer<TKey>.Default;            
            return source
                .Select((x, i) => (key: selector(x), pos: i))
                .Aggregate((x, y) => comparer.Compare(x.key, y.key) > 0 ? x : y).pos;
        }

        public static int ArgMin<T>(this IEnumerable<T> values) => ArgMin(values, _ => _);
        public static int ArgMax<T>(this IEnumerable<T> values) => ArgMin(values, _ => _);
        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, decimal?> selector) => ArgMin<T, decimal?>(values, selector);
        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, decimal> selector) => ArgMin<T, decimal>(values, selector);
        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, double?> selector) => ArgMin<T, double?>(values, selector);
        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, double> selector) => ArgMin<T, double>(values, selector);
        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, float?> selector) => ArgMin<T, float?>(values, selector);
        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, float> selector) => ArgMin<T, float>(values, selector);
        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, int?> selector) => ArgMin<T, int?>(values, selector);
        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, int> selector) => ArgMin<T, int>(values, selector);
        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, long?> selector) => ArgMin<T, long?>(values, selector);
        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, long> selector) => ArgMin<T, long>(values, selector);

        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, decimal?> selector) => ArgMax<T, decimal?>(values, selector);
        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, decimal> selector) => ArgMax<T, decimal>(values, selector);
        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, double?> selector) => ArgMax<T, double?>(values, selector);
        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, double> selector) => ArgMax<T, double>(values, selector);
        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, float?> selector) => ArgMax<T, float?>(values, selector);
        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, float> selector) => ArgMax<T, float>(values, selector);
        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, int?> selector) => ArgMax<T, int?>(values, selector);
        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, int> selector) => ArgMax<T, int>(values, selector);
        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, long?> selector) => ArgMax<T, long?>(values, selector);
        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, long> selector) => ArgMax<T, long>(values, selector);

        #endregion


        /// <summary>
        ///   Checks whether all items in the enumerable are same (Uses <see cref="object.Equals(object)" /> to check for equality)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>
        ///   Returns true if there is 0 or 1 item in the enumerable or if all items in the enumerable are same (equal to
        ///   each other) otherwise false.
        /// </returns>
        /// https://stackoverflow.com/questions/5307172/check-if-all-items-are-the-same-in-a-list
        public static bool AreAllSame<T, K>(this IEnumerable<T> enumerable, Func<T, K> selector)
        {
            Validation.ThrowIfNull(enumerable, new ArgumentNullException(nameof(enumerable)));

            using (var enumerator = enumerable.GetEnumerator()) 
            {
                if (!enumerator.MoveNext())
                    return true;

                var toCompare = selector(enumerator.Current);

                while (enumerator.MoveNext())
                    if (toCompare == null && enumerator.Current != null)
                        return false;
                    else if (!toCompare.Equals(selector(enumerator.Current)))
                        return false;
            }
            return true;
        }

        public static bool AreAllSame<T>(this IEnumerable<T> enumerable) => AreAllSame(enumerable, _ => _);

    }
}
