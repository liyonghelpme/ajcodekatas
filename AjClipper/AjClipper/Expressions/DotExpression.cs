using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjClipper.Expressions
{
    public class DotExpression : IExpression
    {
        private IExpression expression;
        private string name;
        private ICollection<IExpression> arguments;

        public DotExpression(IExpression expression, string name)
            : this(expression, name, null)
        {
        }

        public DotExpression(IExpression expression, string name, ICollection<IExpression> arguments)
        {
            this.expression = expression;
            this.name = name;
            this.arguments = arguments;
        }

        public string Name { get { return this.name; } }

        public IExpression Expression { get { return this.expression; } }

        public ICollection<IExpression> Arguments { get { return this.arguments; } }

        public object Evaluate(ValueEnvironment environment)
        {
            object obj = null;

            Type type = AsType(this.expression, environment);
            
            if (type == null)
                obj = this.expression.Evaluate(environment);

            object[] parameters = null;

            if (this.arguments != null)
            {
                List<object> values = new List<object>();

                foreach (IExpression argument in this.arguments)
                    values.Add(argument.Evaluate(environment));

                parameters = values.ToArray();
            }

            if (type != null)
                return TypeUtilities.InvokeTypeMember(type, this.name, parameters);

            // TODO if undefined, do nothing
            if (obj == null)
                return null;

            return ObjectUtilities.GetValue(obj, this.name, parameters);
        }

        private static Type AsType(IExpression expression, ValueEnvironment environment)
        {
            string name = EvaluateUtilities.EvaluateAsName(expression, environment);

            if (name == null)
                return null;

            return TypeUtilities.AsType(name);
        }

        private static string AsName(IExpression expression)
        {
            if (expression is NameExpression)
                return ((NameExpression)expression).Name;

            if (expression is DotExpression)
            {
                DotExpression dot = (DotExpression)expression;

                return AsName(dot.Expression) + "." + dot.Name;
            }

            return null;
        }
    }
}
