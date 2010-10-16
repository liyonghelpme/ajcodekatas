using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Interpreter;

namespace Patterns.Decorator
{
    public class DebugContext : IContext
    {
        IContext context;
        public delegate void ContextHandler(string name, object value);
        public event ContextHandler GettingValue;
        public event ContextHandler SettingValue;

        public DebugContext(IContext context)
        {
            this.context = context;
        }

        public object GetValue(string name)
        {
            object result = this.context.GetValue(name);
            if (this.GettingValue!=null)
                this.GettingValue(name, result);
            return result;
        }

        public void SetValue(string name, object value)
        {
            if (this.SettingValue != null)
                this.SettingValue(name, value);
            this.context.SetValue(name, value);
        }
    }
}
