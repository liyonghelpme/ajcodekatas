namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;
    using AjClipper.Language;

    public class DoProcedureCommand : BaseCommand
    {
        private string name;
        private IList<IExpression> arguments;

        public DoProcedureCommand(string name, IList<IExpression> arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }

        public string Name { get { return this.name; } }

        public ICollection<IExpression> Arguments { get { return this.arguments; } }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            IList<object> values = new List<object>();

            foreach (IExpression argument in this.arguments)
                values.Add(argument.Evaluate(environment));

            Procedure procedure = (Procedure)environment.GetValue(this.name);

            procedure.Apply(values);
        }
    }
}
