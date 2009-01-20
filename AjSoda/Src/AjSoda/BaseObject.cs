namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class BaseObject : IObject
    {
        private object[] values;

        public BaseObject()
        {
        }

        public BaseObject(int size)
        {
            this.values = new object[size];
        }

        public IBehavior Behavior { get; set; }

        public object Send(string selector, object[] arguments)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            if (this.Behavior == null)
            {
                throw new InvalidOperationException("No behavior in object");
            }

            IMethod method = this.Behavior.Lookup(selector);

            if (method == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unknown message '{0}'", selector));
            }

            return method.Execute(this, arguments);
        }

        public void SetValueAt(int position, object value)
        {
            this.values[position] = value;
        }

        public object GetValueAt(int position)
        {
            return this.values[position];
        }
    }
}
