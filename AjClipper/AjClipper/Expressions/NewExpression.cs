namespace AjClipper.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Language;

    public class NewExpression : IExpression
    {
        private string name;
        private ICollection<IExpression> arguments;

        public NewExpression(string name, ICollection<IExpression> arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }

        public string TypeName { get { return this.name; } }

        public ICollection<IExpression> Arguments { get { return this.arguments; } }

        public object Evaluate(ValueEnvironment environment)
        {
            object value = environment.GetValue(this.name);

            Type type = null;

            type = TypeUtilities.GetType(environment, this.name);

            object[] parameters = null;

            if (this.arguments != null && this.arguments.Count > 0)
            {
                List<object> values = new List<object>();

                foreach (IExpression argument in this.arguments)
                    values.Add(argument.Evaluate(environment));

                parameters = values.ToArray();
            }

            return Activator.CreateInstance(type, parameters);
        }
    }
}
