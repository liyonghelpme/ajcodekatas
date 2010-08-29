using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter
{
    public class VariableExpression : IExpression
    {
        private string name;

        public VariableExpression(string name)
        {
            this.name = name;
        }

        public object Evaluate(BindingEnvironment environment)
        {
            return environment.GetValue(this.name);
        }
    }
}
