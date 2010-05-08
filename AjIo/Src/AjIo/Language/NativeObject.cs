namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NativeObject : IObject
    {
        private object obj;

        public NativeObject(object obj)
        {
            this.obj = obj;
        }

        public object Object { get { return this.obj; } }

        public string TypeName
        {
            get { throw new NotImplementedException(); }
        }

        public IObject Self
        {
            get { throw new NotImplementedException(); }
        }

        public IObject Parent
        {
            get { return null; }
        }

        public object Evaluate(object expression)
        {
            throw new NotImplementedException();
        }

        public void SetSlot(string name, object value)
        {
            throw new NotImplementedException();
        }

        public object GetSlot(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateSlot(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}

