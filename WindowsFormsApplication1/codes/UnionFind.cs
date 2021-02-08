using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.codes
{
    class UnionFind
    {
        private int[] ids; // vertexlerin id si
        private int[] sizeSets;// ağacı dengelemek için eleman sayısı
        public UnionFind(int numElements)
        {
            ids = new int[numElements];
            sizeSets = new int[numElements];

            for (int i = 0; i < numElements; i++)
            {
                ids[i] = i;
                sizeSets[i] = 1;
            }
        }

        /** verilen elemanın kökünü bulma */
        public int find(int x)
        {
            while (x != ids[x])
            {
                ids[x] = ids[ids[x]]; 
                x = ids[x];
            }
            return x;
        }

        /** 2 elemanı birleştirme */
        public void union(int x, int y)
        {
            int xRoot = find(x);
            int yRoot = find(y);

            if (xRoot != yRoot)
            {
                if (sizeSets[xRoot] > sizeSets[yRoot]) // ağacı dengelemek için
                {
                    ids[yRoot] = xRoot;
                    sizeSets[xRoot] += sizeSets[yRoot];
                }
                else
                {
                    ids[xRoot] = yRoot;
                    sizeSets[yRoot] += sizeSets[xRoot];
                }
            }
        }

        /** alt küme sayısı bulma */
        public int getNumberSets()
        {
            int cont = 0;
            for (int i = 0; i < ids.Length; i++)
            {
                if (i == ids[i])
                    cont += 1;
            }
            return cont;
        }

        /** alt küme köklerini bulma */
        public List<int> getRoots()
        {
            List<int> roots = new List<int>();
            for (int i = 0; i < ids.Length; i++)
            {
                if (i == ids[i])
                    roots.Add(i);
            }
            return roots;
        }
	}

    }
