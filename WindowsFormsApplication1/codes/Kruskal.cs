using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.codes;

namespace Project.codes
{
    class Kruskal
    {
        List<Project.codes.Graph.Vertex> initials;// kök noktalar

        public Kruskal()
        {
            initials = new List<Project.codes.Graph.Vertex>();
        }

        public List<Project.codes.Graph.Edge> getMST(Graph graph, int k)
        {
            UnionFind uf = new UnionFind(graph.getGraphSize());
            List<Graph.Edge> edgeList = graph.getEdgeList();
            List<Graph.Edge> mst = new List<Graph.Edge>();
            object[] changes=edgeList.ToArray(); //listeyi objeye dönüştür
            object[] edgetoarray = Heap.Sort(changes); //objeyi sırala
            List<Object> aa=new List<Object>();
            aa = edgetoarray.ToList();
            edgeList = aa.Cast<Graph.Edge>().ToList();//sıralı objeyi edge listesine çevir
            int x;
            int y;
            for (int i = 0; i < edgeList.Count; i++)
            {
                if (k == uf.getNumberSets())// k tane kümeyi doğrulama
                    break;
                x = edgeList[i].getBegin().getId();
                y = edgeList[i].getAdjacent().getId();
                if (uf.find(x) != uf.find(y))
                {
                    uf.union(x, y);
                    mst.Add(edgeList[i]);
                }
            }

            List<int> roots = uf.getRoots();//kökleri bulmak için
            for (int i = 0; i < roots.Count; i++)
            {
                initials.Add(graph.getVertexById(roots[i]));// kök noktaları ekleme
            }
            return mst;
        }

        /** Vertexes rootlarını geri döndürür */
        public List<Project.codes.Graph.Vertex> getInitials()
        {
            return initials; 
        }


    }
}
