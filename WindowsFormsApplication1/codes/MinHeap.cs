using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.codes
{
    public class MinHeap
    {

        private Key[] keys;
        private int n;
      
        public MinHeap(int capacity) {
            keys = new Key[capacity + 1];
            n = 0;
        }

        public MinHeap() : this(1) {
        }
        
        public MinHeap(IComparable[] elements) {
            keys = new Key[elements.Length + 1];
            n = elements.Length;
            for (int i = 0; i < n; i++) {
                keys[i + 1] = new Key(elements[i]);
            }
            constructHeap();
        }

        public bool isEmpty() {
            if (n == 0) return true;
            return false;
        }

        public int getSize() {
            return n;
        }

        public object getMin() {
            if (isEmpty())
                throw new NoSuchElementException("No Elements in the heap now!!!");
            return keys[1].Element;
        }

        private void setCapacity(int newCapacity) {
            if (keys.Length >= newCapacity) return;
            keys = copyElements(newCapacity);
        }

        private void shrinkCapacity(int newCapacity)
        {
            if (newCapacity < n) return;
            keys = copyElements(newCapacity);
        }

        private Key[] copyElements(int newCapacity)
        {
            Key[] tempArr = new Key[newCapacity];
            for (int i = 0; i < n; i++)
            {
                tempArr[i + 1] = keys[i + 1];
            }
            return tempArr;
        }

        public void insertElement(IComparable element) {
            if (n == keys.Length - 1)
                setCapacity(keys.Length * 2);
            keys[++n] = new Key(element);
            topDownConstructHeap(n);
        }

        public object deleteMin() {
            if (isEmpty())
                throw new NoSuchElementException("No Elements in the heap now!!!");
            swap(1, n);
            Key minKey = keys[n--];
            keys[n + 1] = null;
            heapify(1);
            if ((n > 0) && (n == (keys.Length - 1) / 4))
                shrinkCapacity(keys.Length / 2);
            return minKey.Element;
        }

        private void topDownConstructHeap(int index) {
            while ((index > 1) && keys[index/2].CompareTo(keys[index])==1 ) {
                swap(index, index / 2);
                index = index / 2;
            }
        }

        private void heapify(int rootIndex) {
            while (2 * rootIndex <= n) {
                int leftChildIndex = 2 * rootIndex;
                int rightChildIndex = leftChildIndex + 1;
                int elementToChangeIndex = leftChildIndex;
                if (n >= rightChildIndex) {
                    if (keys[leftChildIndex].CompareTo(keys[rightChildIndex]) == 1)
                        elementToChangeIndex = rightChildIndex;
                }

                if (keys[elementToChangeIndex].CompareTo(keys[rootIndex]) == 1) break;
                swap(rootIndex, elementToChangeIndex);
                rootIndex = elementToChangeIndex;
            }
        }

        private void constructHeap() {
            for (int keyIndex = n / 2; keyIndex >= 1; keyIndex--)
                heapify(keyIndex);
        }

        private void swap(int i, int j) {
            Key temp = keys[i];
            keys[i] = keys[j];
            keys[j] = temp;
        }

        
    }
 
}
