using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections;


namespace Project.codes
{
    class Graph
    {
        private Dictionary<Vertex, List<Edge>> adjacents; //komşular  
        private List<Vertex> vertexes;                    //vertexler


        public Graph()
        {
            adjacents = new Dictionary<Vertex, List<Edge>>();
            vertexes = new List<Vertex>();
        }

        /** Vertex oluşturma */
        private void addVertex(int id, double x, double y)
        {
            Vertex node = new Vertex(id, x, y);
            adjacents[node] = new List<Edge>();
            vertexes.Add(node);
        }

        /** Vertexler arası edge oluşturma */
        private void addEdge(Vertex a, Vertex b)
        {
            double distance = euclidianDistance(a, b);
            adjacents[a].Add(new Edge(a, b, distance));
            adjacents[b].Add(new Edge(b, a, distance));
        }

        /** Vertexler için edge ler oluşturma*/
        public void generateEdges()
        {
            for (int i = 0; i < vertexes.Count; i++)
            {
                for (int j = i + 1; j < vertexes.Count; j++)
                {
                    addEdge(vertexes[i], vertexes[j]);
                }
            }
        }
        int cont1 = 0;
        public void generateGraph(double x, double y)
        {
            addVertex(cont1, x, y);
            cont1 += 1;

        }

        public List<Vertex> getVertexList()
        {
            return vertexes;
        }

        /** Tüm Edge leri getirme   */
        public List<Edge> getEdgeList()
        {
            List<Edge> tmp = new List<Edge>();
            for (int i = 0; i < vertexes.Count; i++)
            {
                tmp.AddRange(adjacents[vertexes[i]]);
            }
            return tmp;
        }

        public Vertex getRandomVertex()
        {
            return vertexes[0];
        }

        public Vertex getVertexById(int i)
        {
            return vertexes[i];
        }

        public List<Edge> getAdjacents(Vertex v)
        {
            return adjacents[v];
        }

        /** Vertexler arası edge bulma */
        public Edge getEdge(Vertex a, Vertex b)
        {
            List<Edge> tmp = adjacents[a];
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].getAdjacent().Equals(b))
                    return tmp[i];
            }
            return null;
        }

        public int getGraphSize()
        {
            return vertexes.Count;
        }

        /** Vertexler arasındaki uzaklığı bulma */
        public double euclidianDistance(Vertex a, Vertex b)
        {

            return Math.Sqrt(Math.Pow((a.getX() - b.getX()), 2) + Math.Pow((a.getY() - b.getY()), 2));
        }

        /**
         * Graph için Vertex sınıfı
         */
        public class Vertex : IEquatable<Vertex>
        {
            private int id;
            private double x, y, priority;// the priority attribute is used in the Heap

            public Vertex(int id, double x, double y)
            {
                this.id = id;
                this.x = x;
                this.y = y;
                priority = Double.PositiveInfinity;
            }


            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Vertex objAsPart = obj as Vertex;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }

            public int getId()
            {
                return id;
            }

            public double getX()
            {
                return x;
            }

            public double getY()
            {
                return y;
            }

            public double getPriority()
            {
                return priority;
            }

            public void setPriority(double newPriority)
            {
                priority = newPriority;
            }

            public override int GetHashCode()
            {
                return id;
            }

            public bool Equals(Vertex other)
            {
                if (other == null) return false;
                return (this.id.Equals(other.id));
            }
        }

        /**
         * Graph için Edge sınıfı
         */

        public class Edge : IComparable,IEquatable<Edge>     
        {
            public string EdgeName { get; set; }
            public double EdgeDistance { get; set; }
            public int id { get; set; }
            private Vertex begin;
            private Vertex adjacent;
            private bool labeled = false;

            public Edge(Vertex begin, Vertex adjacent, double distance)
            {
                this.begin = begin;
                this.adjacent = adjacent;
                this.EdgeDistance = distance;
            }

            public Vertex getAdjacent()
            {
                return adjacent;
            }

            public Vertex getBegin()
            {
                return begin;
            }

            public double getDistance()
            {
                return EdgeDistance;
            }

            public bool isLabeled()
            {
                return labeled;
            }

            public void setLabeled()
            {
                labeled = true;
            }

            public override string ToString()
            {
                return "ID: " + id + "   Name: " + EdgeName;
            }
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Edge objAsPart = obj as Edge;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }
            public int SortByNameAscending(string name1, string name2)
            {

                return name1.CompareTo(name2);
            }
            public int CompareTo(object obj)
            {
                Edge other = (Edge)obj;
                if (this.EdgeDistance > other.EdgeDistance) return 1;
                if (this.EdgeDistance < other.EdgeDistance) return -1;
                return 0;
            }
            public override int GetHashCode()
            {
                return id;
            }
            public bool Equals(Edge other)
            {
                if (other == null) return false;
                return (this.EdgeDistance.Equals(other.EdgeDistance));
            }

        }
    }
}

