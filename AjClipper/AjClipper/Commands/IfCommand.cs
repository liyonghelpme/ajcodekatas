namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;

    public class IfCommand : BaseCommand
    {
        private List<IExpression> conditions = new List<IExpression>();
        private List<ICommand> commands = new List<ICommand>();

        public IfCommand()
        {
        }

        public void AddConditionAndCommand(IExpression expression, ICommand command)
        {
            this.conditions.Add(expression);
            this.commands.Add(command);
        }

        public void AddElseCommand(ICommand command)
        {
            this.commands.Add(command);
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            int nc = 0;

            foreach (IExpression condition in this.conditions)
            {
                ICommand command = this.commands[nc];

                if (ExpressionUtilities.IsTrue(condition.Evaluate(environment)))
                {
                    command.Execute(machine, environment);
                    return;
                }

                nc++;
            }

            if (this.commands.Count > this.conditions.Count)
                this.commands[this.conditions.Count].Execute(machine, environment);
        }
    }
}
