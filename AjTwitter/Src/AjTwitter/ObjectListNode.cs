namespace AjTwitter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ObjectListNode<T>
    {
        private const int DefaultSize = 20;

        private T[] elements;
        private int count;

        public ObjectListNode()
            : this(DefaultSize)
        {
        }

        public ObjectListNode(int size)
        {
            this.elements = new T[size];
        }

        public ObjectListNode<T> Next { get; set; }

        public T[] Elements
        {
            get
            {
                return this.elements;
            }
        }

        public bool IsFull
        {
            get
            {
                return this.count == this.elements.Length;
            }
        }

        public void Add(T element)
        {
            this.elements[this.count++] = element;
        }
    }
}

