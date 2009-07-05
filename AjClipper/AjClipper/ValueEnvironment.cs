namespace AjClipper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ValueEnvironment
    {
        private ValueEnvironment parent;
        private Dictionary<string, object> values = new Dictionary<string, object>();

        public ValueEnvironment()
        {
        }

        public ValueEnvironment(ValueEnvironment parent)
        {
            this.parent = parent;
        }

        public void SetValue(string key, object value)
        {
            this.values[key] = value;
        }

        public object GetValue(string key)
        {
            if (!this.values.ContainsKey(key))
            {
                if (this.parent != null)
                    return this.parent.GetValue(key);

                return null;
            }

            return this.values[key];
        }
    }
}
