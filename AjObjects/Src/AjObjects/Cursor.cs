namespace AjObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Cursor : IEnumerator<BasicObject>
    {
        private IEnumerator<BasicObject> enumerator;

        public Cursor(IEnumerable<BasicObject> objects)
        {
            this.enumerator = (new List<BasicObject>(objects)).GetEnumerator();
        }

        public BasicObject Current
        {
            get { return this.enumerator.Current; }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.enumerator.Current; }
        }

        public void Dispose()
        {
            this.enumerator.Dispose();
        }

        public bool MoveNext()
        {
            return this.enumerator.MoveNext();
        }

        public void Reset()
        {
            this.enumerator.Reset();
        }
    }
}
