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
            conditions.Add(expression);
            commands.Add(command);
        }

        public void AddElseCommand(ICommand command)
        {
            commands.Add(command);
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            int nc = 0;

            foreach (IExpression condition in conditions)
            {
                ICommand command = commands[nc];

                if (ExpressionUtilities.IsTrue(condition.Evaluate(environment)))
                {
                    command.Execute(machine, environment);
                    return;
                }

                nc++;
            }

            if (commands.Count > conditions.Count)
                commands[conditions.Count].Execute(machine, environment);
        }
    }
}
