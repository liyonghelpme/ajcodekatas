namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;

    public class PrintLineCommand : BaseCommand
    {
        public IExpression expression;

        public PrintLineCommand(IExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            this.expression = expression;
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            System.Console.WriteLine(this.expression.Evaluate(environment));
        }
    }
}
