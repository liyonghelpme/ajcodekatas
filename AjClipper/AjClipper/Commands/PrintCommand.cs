namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;

    public class PrintCommand : BaseCommand
    {
        private List<IExpression> expressions;

        public PrintCommand(IExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            this.expressions = new List<IExpression>();
            this.expressions.Add(expression);
        }

        public PrintCommand(List<IExpression> expressions)
        {
            this.expressions = expressions;
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            foreach (IExpression expression in this.expressions)
                System.Console.Write(expression.Evaluate(environment));
        }
    }
}
