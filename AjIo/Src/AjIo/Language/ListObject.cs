namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjIo.Methods;

    public class ListObject : ClonedObject, IList<object>
    {
        private IList<object> list;

        public ListObject(IObject parent)
            : base(parent)
        {
            if (parent is ListObject)
                this.list = new List<object>(((ListObject)parent).list);
            else
                this.list = new List<object>();

            this.SetMethodSlot("clone", (context, receiver, arguments) => new ListObject(receiver));
            this.SetMethodSlot("at", (context, receiver, arguments) => ((ListObject)receiver).list[(int)arguments[0]]);
            this.SetMethodSlot("append", (context, receiver, arguments) => { ((ListObject)receiver).list.Add(arguments[0]);  return this; });
            this.SetMethodSlot("remove", (context, receiver, arguments) => { ((ListObject)receiver).list.Remove(arguments[0]); return this; });
            this.SetMethodSlot("atPut", (context, receiver, arguments) => { ((ListObject)receiver).list[(int)arguments[0]] = arguments[1]; return this; });
            this.SetMethodSlot("atInsert", (context, receiver, arguments) => { ((ListObject)receiver).list.Insert((int)arguments[0], arguments[1]); return this; });
            this.SetMethodSlot("size", (context, receiver, arguments) => ((ListObject)receiver).list.Count);
            this.SetSlot("foreach", new ForEachMethod());
        }

        public ListObject(IObject parent, IList<object> elements)
            : this(parent)
        {
            this.list = new List<object>(elements);
        }

        public int IndexOf(object item)
        {
            return this.list.IndexOf(item);
        }

        public void Insert(int index, object item)
        {
            this.list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        public object this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                this.list[index] = value;
            }
        }

        public void Add(object item)
        {
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(object item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(object[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public bool IsReadOnly
        {
            get { return this.list.IsReadOnly; }
        }

        public bool Remove(object item)
        {
            return this.list.Remove(item);
        }

        public IEnumerator<object> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("list(");
            int n = 0;

            foreach (object obj in this.list)
            {
                if (n > 0)
                    builder.Append(", ");

                builder.Append(Machine.PrintString(obj));
                n++;
            }

            builder.Append(")");

            return builder.ToString();
        }
    }
}
