namespace AjLambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Environment
    {
        private Dictionary<string, Expression> values = new Dictionary<string, Expression>();

        public void DefineValue(string name, Expression value) 
        {
            this.values[name] = value;
        }

        public Expression GetValue(string name)
        {
            if (!this.values.ContainsKey(name))
                return null;

            return this.values[name];
        }
    }
}
