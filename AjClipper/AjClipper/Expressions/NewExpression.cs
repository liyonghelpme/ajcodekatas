namespace AjClipper.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Language;

    public class NewExpression : IExpression
    {
        private IExpression nameExpression;
        private ICollection<IExpression> arguments;

        public NewExpression(IExpression nameExpression, ICollection<IExpression> arguments)
        {
            this.nameExpression = nameExpression;
            this.arguments = arguments;
        }

        public ICollection<IExpression> Arguments { get { return this.arguments; } }

        public string TypeName { get { return EvaluateUtilities.EvaluateAsName(this.nameExpression, null); } }

        public object Evaluate(ValueEnvironment environment)
        {
            string name = EvaluateUtilities.EvaluateAsName(this.nameExpression, environment);

            object value = environment.GetValue(name);

            Type type = null;

            type = TypeUtilities.GetType(environment, name);

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
