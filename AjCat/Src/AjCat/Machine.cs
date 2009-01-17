namespace AjCat
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Machine
    {
        private Stack stack = new Stack();

        public int StackCount
        {
            get
            {
                return this.stack.Count;
            }
        }

        public ICollection StackContent
        {
            get
            {
                return this.stack.ToArray();
            }
        }

        public void Push(object value)
        {
            this.stack.Push(value);
        }

        public object Pop()
        {
            return this.stack.Pop();
        }

        public object Top()
        {
            return this.stack.Peek();
        }

        public void Clear()
        {
            this.stack.Clear();
        }
    }
}
