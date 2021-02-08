using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.codes
{
    class Clustering
    {
        public Dictionary<int, List<Graph.Vertex>> clusters;
        private List<Graph.Edge> mst;

        public Clustering(List<Graph.Edge> mst)
        {
            this.mst = mst;
            clusters = new Dictionary<int, List<Graph.Vertex>>();
        }

        /** Küme oluştur	 */
        public void clusterization(List<Graph.Vertex> initials)
        {
            for (int i = 0; i < initials.Count; i++)
            {
                Graph.Vertex initial = initials[i];
                clusters[i]= new List<Graph.Vertex>();
                createCluster(initial, i);
            }
        }

        /** Kümedeki elemanları grupla	 */
        private void createCluster(Graph.Vertex v, int cluster)
        {
            clusters[cluster].Add(v);
            for (int i = 0; i < mst.Count; i++)
            {
                if (!mst[i].isLabeled())
                {
                    if (v.Equals(mst[i].getAdjacent()))
                    {
                        mst[i].setLabeled();
                        createCluster(mst[i].getBegin(), cluster);
                    }
                    else if (v.Equals(mst[i].getBegin()))
                    {
                        mst[i].setLabeled();
                        createCluster(mst[i].getAdjacent(), cluster);
                    }
                }
            }
        }


    }
}
