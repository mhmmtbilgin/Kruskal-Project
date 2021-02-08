using System;

namespace Project.codes
{
    internal class Key : IComparable
    {
        IComparable element;

        public Key(IComparable element) {
            this.element = element;
        }
        
        public object Element {
            get { return (object)element; }
            set { element = (IComparable)value; }
        }

        public int CompareTo(object obj)
        {
            Key other = (Key)obj; 
            return element.CompareTo(other.Element);
        }

        public override string ToString()
        {
            return element.ToString();
        }


    }
}