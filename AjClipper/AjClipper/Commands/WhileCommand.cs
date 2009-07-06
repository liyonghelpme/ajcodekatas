namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;

    public class WhileCommand : BaseCommand
    {
        private IExpression condition;
        private ICommand command;

        public WhileCommand(IExpression condition, ICommand command)
        {
            this.condition = condition;
            this.command = command;
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            while (ExpressionUtilities.IsTrue(this.condition.Evaluate(environment)))
                this.command.Execute(machine, environment);
        }
    }
}
