using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AjClipper.Language;

namespace AjClipper.Expressions
{
    public class InvokeExpression : IExpression
    {
        private string name;
        private ICollection<IExpression> arguments;

        public InvokeExpression(string name)
            : this(name, null)
        {
        }

        public InvokeExpression(string name, ICollection<IExpression> arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }

        public string Name { get { return this.name; } }

        public ICollection<IExpression> Arguments { get { return this.arguments; } }

        public object Evaluate(ValueEnvironment environment)
        {
            Procedure procedure = (Procedure)environment.GetValue(this.name);

            object[] parameters = null;

            if (this.arguments != null)
            {
                List<object> values = new List<object>();

                foreach (IExpression argument in this.arguments)
                    values.Add(argument.Evaluate(environment));

                parameters = values.ToArray();
            }

            return procedure.Apply(parameters, environment);
        }
    }
}
