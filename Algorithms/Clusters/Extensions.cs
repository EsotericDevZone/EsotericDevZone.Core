using System;
using System.Collections.Generic;
using System.Linq;

namespace EsotericDevZone.Core.Algorithms.Clusters
{
    public static class Extensions
    {
        public static int[] ClusterizeMapping(this IEnumerable<int[]> values, int clustersCount)
            => new KMeans<int, double>((u, v) => Math.Sqrt(u.Zip(v, (x, y) => (x - y) * (x - y)).Sum()), distanceEpsilon: 0.1)
                .ClusterizeMapping(values.ToList(), clustersCount);

        public static int[] ClusterizeMapping<T>(this IEnumerable<T> values, Func<T, int[]> vectorizer, int clustersCount)
            => new ObjectKMeans<T, int, double>(vectorizer, (u, v) => Math.Sqrt(u.Zip(v, (x, y) => (int)(x - y) * (x - y)).Sum()), distanceEpsilon: 0.1)
                .ClusterizeMapping(values, clustersCount);
    }
}
