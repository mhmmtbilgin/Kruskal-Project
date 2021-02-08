using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.codes
{
    public class Heap
    {
        private static MinHeap container;

        public static object[] Sort(object[] elements) {
            createHeap(elements);
            object[] sortedArray = new object[elements.Length];
            
            for (int i = 0; i < elements.Length; i++) {
                sortedArray[i] = container.deleteMin();
            }
            return sortedArray;
        }

        private static void createHeap(object[] elements)
        {
            container = new MinHeap((IComparable[])elements);
        }

      
    }
}
