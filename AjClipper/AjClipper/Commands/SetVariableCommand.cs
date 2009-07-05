namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;

    public class SetVariableCommand : BaseCommand
    {
        private string name;
        private IExpression expression;

        public SetVariableCommand(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            environment.SetValue(this.name, this.expression.Evaluate(environment));
        }
    }
}
