using EsotericDevZone.Core.Collections;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsotericDevZone.Core.Algorithms.Clusters
{
    public class KMeans<T, D>
    {
        private readonly Func<T[], T[], D> Distance;
        private readonly IComparer<D> DistanceComparer;
        private readonly D DistanceEpsilon;

        public KMeans(Func<T[], T[], D> distance, IComparer<D> distanceComparer = null, D distanceEpsilon = default)
        {
            Distance = distance;
            DistanceComparer = distanceComparer;
            DistanceEpsilon = distanceEpsilon;
        }

        private (T[][] Centroids, int[] ClustersMapping) DoClusterize(List<T[]> values, int clustersCount, int maxSteps)
        {
            Validation.ThrowIfNull(values, nameof(values));
            Validation.ThrowIf(clustersCount <= 0, "Invalid clusters count. Clusters count must be positive");
            Validation.ThrowIf(clustersCount > values.Count, "Number of clusters should not exceed the number of items");
            Validation.ThrowIf(!values.AreAllSame(v => v.Length), "Dimension mismatch among input values");
            var dim = values[0].Length;
            Validation.ThrowIf(dim == 0, "Number of vector dimensions must be non-zero");

            if (clustersCount == values.Count)
                return (values.ToArray(), Enumerable.Range(0, clustersCount).ToArray());

            var centroids = values.Shuffle().Take(clustersCount).ToArray();            

            var assignedCluster = new int[values.Count];
            var oldDistance = default(D);

            if (maxSteps <= 0)
                maxSteps = 1000000 / (values.Count * dim);
            if (maxSteps == 0)
                maxSteps = 1;

            for (int @_ = 0; @_ < maxSteps; @_++) 
            {                
                for (int i = 0; i < values.Count; i++)
                {
                    assignedCluster[i] = centroids.ArgMin(c => (Distance(c, values[i])), DistanceComparer);
                }
                
                var d = assignedCluster.Select((c, i) => Distance(centroids[c], values[i]))
                    .Aggregate((a, b) => (D)((dynamic)a + (dynamic)b));
                bool canBreak = false;

                if (Math.Abs((dynamic)d - oldDistance) < (dynamic)DistanceEpsilon) 
                {
                    if ((dynamic)d > oldDistance) 
                        break;
                    canBreak = true;
                }

                centroids = values.Zip(assignedCluster, (x, y) => (Item: x, Cluster: y))
                    .GroupBy(x => x.Cluster)
                    .Select(g => (g.Key, Centroid: g
                        .Select(x => x.Item)
                        .Aggregate((i1, i2) => i1.Zip(i2, (a, b) => (T)((dynamic)a + (dynamic)b)).ToArray())
                        .Select(x => (T)(g.Count() == 0 ? 0 : (dynamic)x / (dynamic)g.Count()))
                        .ToArray()))
                    .Select(p => p.Centroid)
                    .ToArray();

                if (canBreak)
                    break;
            }

            return (centroids, assignedCluster);
        }

        public List<(List<T[]> Items, T[] Centroid)> Clusterize(List<T[]> values, int clustersCount, int maxSteps = 0)
        {
            var (centroids, assignedCluster) = DoClusterize(values, clustersCount, maxSteps);
            return centroids
                .Select((c, i) => (assignedCluster.Where(p => p == i).Select(_ => values[_]).ToList(), c))
                .ToList();
        }

        public int[] ClusterizeMapping(List<T[]> values, int clustersCount, int maxSteps = 0)
            => DoClusterize(values, clustersCount, maxSteps).ClustersMapping;

    }
}
