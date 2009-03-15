namespace AjTwitter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ObjectList<T>
    {
        private int count;
        private ObjectListNode<T> firstNode;
        private ObjectListNode<T> lastNode;

        public ObjectList()
        {
            this.lastNode = this.firstNode = new ObjectListNode<T>();
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public virtual IEnumerable<T> Elements
        {
            get
            {
                for (ObjectListNode<T> node = this.firstNode; node != null; node = node.Next)
                {
                    foreach (T element in node.Elements)
                        if (element != null)
                            yield return element;
                }
            }
        }

        protected ObjectListNode<T> FirstNode
        {
            get
            {
                return this.firstNode;
            }

            set
            {
                this.firstNode = value;
            }
        }

        protected ObjectListNode<T> LastNode
        {
            get
            {
                return this.lastNode;
            }

            set
            {
                this.lastNode = value;
            }
        }

        public virtual void Add(T element) 
        {
            lock (this)
            {
                if (this.lastNode.IsFull)
                {
                    ObjectListNode<T> newNode = new ObjectListNode<T>();

                    this.lastNode.Next = newNode;

                    this.lastNode = newNode;
                }

                this.lastNode.Add(element);

                this.count++;
            }
        }

        public void AddFirst(T element)
        {
            lock (this)
            {
                if (this.firstNode.IsFull)
                {
                    ObjectListNode<T> newNode = new ObjectListNode<T>();

                    newNode.Next = this.firstNode;

                    this.firstNode = newNode;
                }

                this.firstNode.Add(element);

                this.count++;
            }
        }

        public void Remove(T element)
        {
            lock (this)
            {
                for (ObjectListNode<T> node = this.firstNode; node != null; node = node.Next)
                {
                    for (int k = 0; k < node.Elements.Length; k++)
                        if (element.Equals(node.Elements[k]))
                        {
                            node.Elements[k] = default(T);
                            this.count--;
                            return;
                        }
                }
            }
        }
    }
}

