namespace Interpreter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BindingEnvironment
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();

        public object GetValue(string name)
        {
            if (this.values.ContainsKey(name))
                return this.values[name];

            return null;
        }

        public void SetValue(string name, object value)
        {
            this.values[name] = value;
        }
    }
}
