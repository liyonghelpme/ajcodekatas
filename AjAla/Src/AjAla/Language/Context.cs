namespace AjAla.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Context : IContext
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();

        public void SetValue(string name, object value)
        {
            if (this.values.ContainsKey(name))
                throw new InvalidOperationException("Value already defined");

            this.values[name] = value;
        }

        public object GetValue(string name)
        {
            return this.values[name];
        }

        public void SetVariable(string name, object value)
        {
            if (this.values.ContainsKey(name))
            {
                object current = this.values[name];
                if (current != null && value != null && current.GetType() != value.GetType())
                    throw new InvalidOperationException("Incompatible value for variable");
            }

            this.values[name] = value;
        }
    }
}
