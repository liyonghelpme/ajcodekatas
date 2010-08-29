using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter
{
    public class BindingEnvironment
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();

        public object GetValue(string name)
        {
            if (values.ContainsKey(name))
                return values[name];

            return null;
        }

        public void SetValue(string name, object value)
        {
            values[name] = value;
        }
    }
}
