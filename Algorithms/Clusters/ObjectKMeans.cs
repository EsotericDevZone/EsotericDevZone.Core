using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsotericDevZone.Core.Algorithms.Clusters
{
    public class ObjectKMeans<C, T, D>
    {
        private readonly Func<C, T[]> Vectorizer;
        private readonly KMeans<T, D> KMeans;

        public ObjectKMeans(Func<C, T[]> vectorizer, Func<T[], T[], D> distance, IComparer<D> distanceComparer = null, D distanceEpsilon = default)
        {
            KMeans = new KMeans<T, D>(distance, distanceComparer, distanceEpsilon);
            Vectorizer = vectorizer;
        }

        public int[] ClusterizeMapping(IEnumerable<C> values, int clustersCount, int maxSteps = 0)
            => KMeans.ClusterizeMapping(values.Select(Vectorizer).ToList(), clustersCount, maxSteps);

        public IEnumerable<C[]> Clusterize(List<C> values, int clustersCount, int maxSteps = 0)
            => ClusterizeMapping(values, clustersCount, maxSteps)
                .Select((c, i) => (Cluster: c, Item: values[i]))
                .GroupBy(_ => _.Cluster)
                .Select(g => g.Select(_ => _.Item).ToArray());
    }
}
